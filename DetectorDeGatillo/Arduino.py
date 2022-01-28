# import serial
# import keyboard as k
#
# try:
#     ser = serial.Serial("COM5", 9600)
#     while True:
#         # print("entro")
#         if k.is_pressed("y"):
#             print("se envio")
#             ser.write(b'y')
#         elif k.is_pressed("n"):
#             ser.write(b'n')
#         elif k.is_pressed("ENTER"):
#             ser.close()
#             break
# except TimeoutError:
#     print("error")
# finally:
#     print("done")
#

# import serial
#
# ser = serial.Serial("COM5", 9600, timeout=1)
# def retri():
#     # print("entro1")
#     ser.write(b'1')
#     data=ser.readline().decode('ascii')
#     return data
# while (True):
#     print('entro2')
#     a=input("dato?")
#     if a=='1':
#         print(retri())
#     else:
#         ser.write(b'0')


# import serial
# import pyautogui
# com="COM4"
# # deArduino = serial.Serial(com, 9600)
#
# deArduino = serial.Serial(com, 9600,timeout=100)
# while True:
#     # print("entro")
#     while (deArduino.inWaiting() == 0):
#         pass
#     datoString = deArduino.readline().decode('ascii')
#     a = datoString.splitlines()
#     # print(a)
#
#     ##Para una variable
#
#     b=str(a[0])
#     c=b.replace("b","")
#     d=c.replace("'","")
#     e=(d)
#     # print(e)
#     # if e=="0":
#     #     print("no disparo")
#     # elif e=="1":
#     #     print("disparo")
#     #     pyautogui.click()
#     try:
#         q = open('datosarduino.json', 'w')
#         q.write(e)
#
#         q.close()
#     except:
#         pass
#
#     with open("datosarduino.json", "r") as archivo:
#         for linea in archivo:
#             print(linea)
#
#
#     # print (e)
#     """
#     ###Para 2 variables
#
#     b=str(a[0])
#     c=b.replace("b","")
#     d=c.replace("'","")
#     e=d.split(",")
#     f1=float(e[0])
#     f2=float(e[1])
#     print (f1,f2)
#     """

#
# import serial
# #
# ww=0
# com='COM4'
# try:
#     ser = serial.Serial("COM4", 9600, timeout=1)
#     ser.flushInput()
#     while True:
#         print("rrr")
#         datoString = ser.readline()
#         a = datoString.splitlines()
#
#         print(a)
#         # print(a)
#         # b = str(a[0])
#         # c=b.replace("b","")
#         # d=c.replace("'","")
#         # e=int(d)
#         #
#         # ww=ww+1
#         # print('hola',ww)
#         # print (e)
#
#         break
# except TimeoutError:
#     print("error")
# finally:
#     print("done")

#
# import cv2
# import numpy as np
# import serial
#
# ser = serial.Serial('COM3', 9600)
#
# def nothing(x):
#     pass
#
# cap = cv2.VideoCapture(0)
# font = cv2.FONT_HERSHEY_COMPLEX                ##Font style for writing text on video frame
# cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)        ##Set camera resolution
# cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)
# Kernal = np.ones((3, 3), np.uint8)
#
# while(1):
#     ret, frame = cap.read()         ##Read image frame
#     frame = cv2.flip(frame, 1)     ##Mirror image frame
#     if not ret:                     ##If frame is not read then exit
#         break
#     if cv2.waitKey(1) == ord('s'):  ##While loop exit condition
#         break
#     frame2 = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)         ##BGR to HSV
#     lb = np.array([153, 119, 212])
#     ub = np.array([255, 255, 255])
#
#     mask = cv2.inRange(frame2, lb, ub)                      ##Create Mask
#     cv2.imshow('Masked Image', mask)
#
#     opening = cv2.morphologyEx(mask, cv2.MORPH_OPEN, Kernal)        ##Morphology
#     cv2.imshow('Opening', opening)
#
#     res = cv2.bitwise_and(frame, frame, mask= opening)             ##Apply mask on original image
#     cv2.imshow('Resuting Image', res)
#
#     contours, hierarchy = cv2.findContours(opening, cv2.RETR_TREE,      ##Find contours
#                                            cv2.CHAIN_APPROX_NONE)
#
#     if len(contours) != 0:
#         cnt = contours[0]
#         area = cv2.contourArea(cnt)
#         distance = 2*(10**(-7))* (area**2) - (0.0067 * area) + 83.487
#         M = cv2.moments(cnt)
#         Cx = int(M['m10']/M['m00'])
#         Cy = int(M['m01'] / M['m00'])
#         S = 'Location' + '(' + str(Cx) + ',' + str(Cy) + ')'
#         cv2.putText(frame, S, (5, 50), font, 2, (0, 0, 255), 2, cv2.LINE_AA)
#         if Cx>=160 and Cy>=160:
#             ser.write(str('h').encode())
#         else:
#             ser.write(str('n').encode())
#     cv2.imshow('Original Image', frame)
#
#
# cap.release()                   ##Release memory
# cv2.destroyAllWindows()         ##Close all the windows
