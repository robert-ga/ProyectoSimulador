import serial
import pyautogui
com = "COM7"
deArduino = serial.Serial(com, 9600, timeout=100)
while True:
    while deArduino.inWaiting() == 0:
        pass
    datoString = deArduino.readline().decode('ascii')
    a = datoString.splitlines()
    b = str(a[0])
    c = b.replace("b", "")
    d = c.replace("'", "")
    e = d
    if e == "1":
        print("se disparo")
        pyautogui.click()




