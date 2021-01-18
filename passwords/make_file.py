with open("raw_passwords", "r") as file:
    with open("ready_passwords.txt", "w") as file_w:
        alls = []
        for f in file:
            words = f.split("\t")
            for i in words[1:]:
                i = i.replace("\n", "")
                alls.append(i)
        print(alls)
        for a in alls:
            file_w.write(a)
            file_w.write("\n")
