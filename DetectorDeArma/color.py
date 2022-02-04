# import numpy as np
# import cv2
# def nada(x):
#     pass
# cv2.namedWindow('parametros')
# cv2.createTrackbar('t.minima','parametros',0,179,nada)
# cv2.createTrackbar('t.maxima','parametros',0,179,nada)
# cv2.createTrackbar('p.minima','parametros',0,255,nada)
# cv2.createTrackbar('p.maxima','parametros',0,255,nada)
# cv2.createTrackbar('l.minima','parametros',0,255,nada)
# cv2.createTrackbar('l.maxima','parametros',0,255,nada)
# cv2.createTrackbar('kermel x','parametros',1,30,nada)
# cv2.createTrackbar('kermel y','parametros',1,30,nada)
#
#
#
#
# cap=cv2.VideoCapture(0)
#
# while True:
#     ret,frame=cap.read()
#     if ret:
#         hsv =cv2.cvtColor(frame,cv2.COLOR_BGR2HSV)
#
#         tmin=cv2.getTrackbarPos('t.minima','parametros')
#         tmax = cv2.getTrackbarPos('t.maxima', 'parametros')
#         pmin = cv2.getTrackbarPos('p.minima', 'parametros')
#         pmax = cv2.getTrackbarPos('p.maxima', 'parametros')
#         lmin = cv2.getTrackbarPos('l.minima', 'parametros')
#         lmax = cv2.getTrackbarPos('l.maxima', 'parametros')
#
#         coloroscuro=np.array([tmin,pmin,lmin])
#         colorbrilla = np.array([tmax, pmax, lmax])
#
#         mascara=cv2.inRange(hsv,coloroscuro,colorbrilla)
#         kermelx=cv2.getTrackbarPos('kermel x', 'parametros')
#         kermely = cv2.getTrackbarPos('kermel y', 'parametros')
#
#         kermel=np.ones((kermelx,kermely), np.uint8)
#
#         mascara = cv2.morphologyEx(mascara, cv2.MORPH_CLOSE,kermel)
#         mascara = cv2.morphologyEx(mascara, cv2.MORPH_OPEN, kermel)
#
#         contorno, _=cv2.findContours(mascara,cv2.RETR_LIST,cv2.CHAIN_APPROX_SIMPLE)
#
#         cv2.drawContours(frame,contorno,-1,(0,0,0),2)
#
#
#         cv2.imshow("camara", frame)
#         cv2.imshow("mascara",mascara)
#
#
#         k=cv2.waitKey(5)
#         if k==27:
#             cv2.destroyAllWindows()
# cap.release()
#




import cv2
import numpy as np

def nothing(x):
    pass

cap = cv2.VideoCapture(0)
font = cv2.FONT_HERSHEY_COMPLEX                ##Font style for writing text on video frame
cap.set(cv2.CAP_PROP_FRAME_WIDTH, 1280)        ##Set camera resolution
cap.set(cv2.CAP_PROP_FRAME_HEIGHT, 720)
Kernal = np.ones((3, 3), np.uint8)

while(1):
    ret, frame = cap.read()         ##Read image frame
    frame = cv2.flip(frame, 1)     ##Mirror image frame
    if not ret:                     ##If frame is not read then exit
        break
    if cv2.waitKey(1) == ord('s'):  ##While loop exit condition
        break
    frame2 = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)         ##BGR to HSV
    lb = np.array([153, 119, 212])
    ub = np.array([255, 255, 255])

    mask = cv2.inRange(frame2, lb, ub)                      ##Create Mask
    cv2.imshow('Masked Image', mask)

    opening = cv2.morphologyEx(mask, cv2.MORPH_OPEN, Kernal)        ##Morphology
    cv2.imshow('Opening', opening)

    res = cv2.bitwise_and(frame, frame, mask= opening)             ##Apply mask on original image
    cv2.imshow('Resuting Image', res)

    contours, hierarchy = cv2.findContours(opening, cv2.RETR_TREE,      ##Find contours
                                           cv2.CHAIN_APPROX_NONE)

    if len(contours) != 0:
        cnt = contours[0]
        area = cv2.contourArea(cnt)
        distance = 2*(10**(-7))* (area**2) - (0.0067 * area) + 83.487
        M = cv2.moments(cnt)
        Cx = int(M['m10']/M['m00'])
        Cy = int(M['m01'] / M['m00'])
        S = 'Location' + '(' + str(Cx) + ',' + str(Cy) + ')'
        cv2.putText(frame, S, (5, 50), font, 2, (0, 0, 255), 2, cv2.LINE_AA)
    cv2.imshow('Original Image', frame)


cap.release()                   ##Release memory
cv2.destroyAllWindows()         ##Close all the windows
