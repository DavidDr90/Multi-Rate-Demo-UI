import os
import threading

print("Start looping!")



def run_cmd(cmd):
    import subprocess
    process = subprocess.run(cmd)


def run_cmd_window(cmd):
    print("2233")
    os.system(f"start /wait cmd /c {cmd}")



base_port = 8000
iperf_cmd = "iperf3.exe -s -p {}"
for i in range(0, 51):
    print(i)
    temp_cmd = iperf_cmd.format(base_port + i) 
    threading.Thread(target=run_cmd_window, args=(temp_cmd,)).start()

print("Finish!")

