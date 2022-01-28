import serial

ser = serial.Serial("COM5", 9600, timeout=1)
def retri():
    print("entro1")
    ser.write(b'1')
    data=ser.readline().decode('ascii')
    return data
while (True):
    print('entro2')
    a=input("dato?")
    if a=='1':
        print(retri())
    else:
        ser.write(b'0')

