import threading
import time
from tkinter import *
from tkinter import ttk
from tkinter.filedialog import askopenfilename
import tkinter as tk

from password_breaker import PassWordBreaker


class GUI:

    def __init__(self):
        self.root = Tk()
        self.root.title = "PassWord Breaker"
        self.breaker = None

        self.breaker_thread = None
        self.read_logins = list()
        self.read_passwords = list()

        self.app_status = StringVar()
        self.app_status.set("IDLE")
        self.concurrency = IntVar()

        self.login_file = StringVar()
        # self.login_file.set("vgwgsrnpxyybjnahtz@twzhhq.online")
        self.login_file.set("ofmnsspoipslvozlif@upived.online")
        self.password_file = StringVar()
        self.password_file.set("C:/Users/Szymon/PycharmProjects/passwordBreaker/passwords/tests.txt")
        self.url = StringVar()
        self.url.set("https://nk.pl/logowanie")

        self.ok_pattern_text = tk.Text(self.root, width=30, height=10)
        self.ok_pattern_text.insert(tk.END, "<!DOCTYPE")
        self.ok_pattern_text.grid(column=0, row=6, sticky=(N, W, E, S), padx=10, pady=3)

        self.bad_pattern_text = tk.Text(self.root, width=30, height=10)
        self.bad_pattern_text.insert(tk.END, "\n<!DOCTYPE")
        self.bad_pattern_text.grid(column=1, row=6, sticky=(N, W, E, S), padx=10, pady=3)

        self.hacked_passwords = []
        self.hacked_logins = None

        self.clear_save_file()

    def clear_save_file(self):
        with open("hacked_passwords.txt", "w") as file:
            file.write("")

    def read_file(self, filename):
        try:
            with open(filename, "r") as file:
                return file.read().split("\n")
        except FileNotFoundError:
            return filename.split(";")

    def write_hacked(self):
        while True:
            with open("hacked_passwords.txt", "r") as file:
                self.hacked_logins.delete(*self.hacked_logins.get_children())
                for i, line in enumerate(file.readlines(), 1):
                    self.hacked_logins.insert("", "end", text=i, values=line.split(":"))
            time.sleep(2)

    def break_passwords(self):
        self.breaker = PassWordBreaker(
            good_pattern=self.ok_pattern_text.get("1.0", tk.END),
            bad_pattern=self.bad_pattern_text.get("1.0", tk.END),
            concurrency=self.concurrency.get()
        )
        threading.Thread(target=self.write_hacked).start()
        self.breaker.break_password(self.read_logins, self.read_passwords, self.url.get())

    def start_breaking(self):
        self.read_logins = self.read_file(self.login_file.get())
        self.read_passwords = self.read_file(self.password_file.get())

        self.breaker_thread = threading.Thread(target=self.break_passwords).start()

    def stop_breaking(self):
        self.clear_save_file()
        self.breaker.stop()

    def select_login_file(self):
        filename = askopenfilename()
        self.login_file.set(filename)

    def select_password_file(self):
        filename = askopenfilename()
        self.password_file.set(filename)

    def set_concurrency(self):
        self.concurrency = True

    def gui(self):

        # Login
        login = ttk.Label(self.root, text="Login")
        login.grid(column=0, row=2)

        select_login_button = ttk.Button(self.root, text='Load logins', command=self.select_login_file)
        select_login_button.grid(column=1, row=2, sticky=(N, W, E, S), padx=10, pady=3)

        login_file_entry = ttk.Entry(self.root, textvariable=self.login_file)
        login_file_entry.grid(column=2, row=2, columnspan=20, sticky=(N, W, E, S), padx=10, pady=3)
        self.root.columnconfigure(1, weight=1)

        # Password
        password = ttk.Label(self.root, text="Password")
        password.grid(column=0, row=3)

        select_password_button = ttk.Button(self.root, text='Load passwords', command=self.select_password_file)
        select_password_button.grid(column=1, row=3, sticky=(N, W, E, S), padx=10, pady=3)

        login_file_entry = ttk.Entry(self.root, textvariable=self.password_file)
        login_file_entry.grid(column=2, row=3, columnspan=20, sticky=(N, W, E, S), padx=10, pady=3)
        self.root.columnconfigure(1, weight=1)

        # URL
        url = ttk.Label(self.root, text="URL")
        url.grid(column=0, row=4, columnspan=20)

        login_file_entry = ttk.Entry(self.root, textvariable=self.url)
        login_file_entry.grid(column=2, row=5, columnspan=20, sticky=(N, W, E, S), padx=10, pady=3)
        self.root.columnconfigure(1, weight=1)

        pattern_ok = ttk.Label(self.root, text="Pass")
        pattern_ok.grid(column=0, row=5)
        self.root.columnconfigure(1, weight=1)

        pattern_bad = ttk.Label(self.root, text="Fail")
        pattern_bad.grid(column=1, row=5)
        self.root.columnconfigure(1, weight=1)

        self.hacked_logins = ttk.Treeview(self.root, columns=("login", "password"))
        self.hacked_logins.grid(column=50, row=0, rowspan=30, padx=(10, 20), columnspan=2)
        self.hacked_logins.column("#0", width=40, anchor=W)
        self.hacked_logins.heading("login", text="Login")
        self.hacked_logins.column("login", width=230)
        self.hacked_logins.heading("password", text="Password")

        # Start
        start_button = ttk.Button(self.root, text='Start', command=self.start_breaking)
        start_button.grid(column=50, row=8, sticky=(N, W, E, S), padx=(60, 10))

        # Stop
        stop_button = ttk.Button(self.root, text='Stop', command=self.stop_breaking)
        stop_button.grid(column=51, row=8, sticky=(N, W, E, S), padx=(10, 60))

        # Stop
        check_button = ttk.Checkbutton(self.root, variable=self.concurrency, offvalue=0, onvalue=1, text="Concurrency")
        check_button.grid(column=50, row=9, sticky=(N, W, E, S), padx=(60, 10), pady=10)

        self.root.mainloop()
