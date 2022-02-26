# # pip install pypiwin32, numpy, opencv-python
#
# import win32api
# import win32con
#
# import time
# import os
#
# import numpy as np
# import cv2
#
#
# # For windows
# def simulateMouseClick(pos):
#     # Cursor position is integer (current pixel)
#     intPos = (int(pos[0]), int(pos[1]))
#     win32api.SetCursorPos(intPos)
#     win32api.mouse_event(win32con.MOUSEEVENTF_LEFTDOWN, 0, 0)
#     win32api.mouse_event(win32con.MOUSEEVENTF_LEFTUP, 0, 0)
#
#
# def currentMillis():
#     return int(round(time.time() * 1000))
#
#
# # Initialize first webcam with parameters
# # sufficient to detect brightness change
# # on an already bright surface, when a laser hits it
# cam = cv2.VideoCapture(0)
#
# cam.set(cv2.CAP_PROP_BRIGHTNESS, 10)
# cam.set(cv2.CAP_PROP_EXPOSURE, 60)
# cam.set(cv2.CAP_PROP_CONTRAST, 40)  # 30 -> default
# cam.set(cv2.CAP_PROP_GAIN, 25)  # 30~40 -> projector/screen, 20~30 -> laser
# cam.set(cv2.CAP_PROP_SATURATION, 40)
#
# totalFrames = 0
#
# undetectedLaserFrames = 0
#
# laserInactivityThreshold = 5  # frames
# maxValueThreshold = 140  # Brightness for laser
#
# # On the PC
# screenWidth = 1920
# screenHeight = 1080
#
# # Debug calibration circles color
# calibrationColor = (0, 255, 0)
#
# maxLocPrev = (-1, -1)
# laserPrev = False
#
# # Calibration corners
# topLeft = (-1, -1)
# botRight = (-1, -1)
#
#
# def isTopCalibrated():
#     return topLeft[0] != -1 and topLeft[1] != -1
#
#
# def isBotCalibrated():
#     return botRight[0] != -1 and botRight[1] != -1
#
#
# def isCalibrated():
#     return isTopCalibrated() and isBotCalibrated()
#
#
# def isPrevMaxLocValid():
#     return maxLocPrev[0] != -1 and maxLocPrev[1] != -1
#
#
# def getCalibratedWidth():
#     return botRight[0] - topLeft[0]
#
#
# def getCalibratedHeight():
#     return botRight[1] - topLeft[1]
#
#
# while (True):
#     ret, frame = cam.read()
#
#     hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
#
#     # find the colors within the specified boundaries and apply mask
#     mask = cv2.inRange(hsv, np.array([102, 70, 36]), np.array([143, 255, 255]))  # we only want purple
#     hsv = cv2.bitwise_and(hsv, hsv, mask=mask)
#
#     h, s, v = cv2.split(hsv)
#     (minValue, maxValue, minLoc, maxLoc) = cv2.minMaxLoc(v)
#
#     # print("Undetected laser frames: {}".format(undetectedLaserFrames))
#
#     if (maxValue > maxValueThreshold):  # Possible laser at 'maxLoc'
#         # print("Possible laser at maxLoc")
#         if (laserPrev == False):  # and undetectedLaserFrames >= laserInactivityThreshold): # Shot
#             print("Laser -> ON")
#             laserPrev = True
#             if (isCalibrated()):  # Calibration is negative edge triggered, so no else
#                 locX = (screenWidth / getCalibratedWidth()) * (maxLoc[0] - topLeft[0])
#                 locY = (screenHeight / getCalibratedHeight()) * (maxLoc[1] - topLeft[1])
#                 simulateMouseClick((locX, locY))
#
#         # undetectedLaserFrames = 0
#     else:  # If no laser
#         if (laserPrev == True):  # Negative edge calibration trigger
#             print("Laser -> OFF")
#             if (not isCalibrated()):
#                 print("Not fully calibrated,")
#                 if (not isPrevMaxLocValid()):
#                     print("Can't calibrate, prevMaxLoc is not valid")
#                     continue  # ?
#                 else:  # Calibrate
#                     if (not isTopCalibrated()):
#                         print("Calibrating Top Left Corner")
#                         topLeft = maxLocPrev
#                     elif (not isBotCalibrated()):
#                         print("Calibrating Bot Left Corner")
#                         botRight = maxLocPrev
#
#         laserPrev = False
#         # undetectedLaserFrames += 1
#
#     if (laserPrev == True):
#         maxLocPrev = maxLoc
#
#     # Draw calibration edges on actual webcam
#     if (isTopCalibrated()):
#         cv2.circle(frame, topLeft, 5, calibrationColor, -1)
#     if (isBotCalibrated()):  # Top would be already calibrated
#         cv2.circle(frame, botRight, 5, (0, 255, 0), -1)
#         cv2.circle(frame, (topLeft[0] + getCalibratedWidth(), topLeft[1]), 5, calibrationColor, -1)  # Top Right
#         cv2.circle(frame, (topLeft[0], botRight[1]), 5, calibrationColor, -1)  # Bot Left
#
#     cv2.imshow('Configured Webcam', frame)
#     # cv2.imshow('HSV', hsv) # Masked HSV won't show a proper image
#     cv2.imshow('V - Brightness', v)  # Useful but will ditch in future
#
#     totalFrames += 1
#
#     # Till key 'q' is pressed
#     if cv2.waitKey(1) == ord('q'):
#         break
#
# # Cleanup
# cam.release()
# cv2.destroyAllWindows()

#
#
# import cv2
# import numpy as np
# cap = cv2.VideoCapture(0)
#
# pts = []
# while (1):
#
#     # Take each frame
#     ret, frame = cap.read()
#     hsv = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
#
#     lower_red = np.array([0, 0, 255])
#     upper_red = np.array([255, 255, 255])
#     mask = cv2.inRange(hsv, lower_red, upper_red)
#     (minVal, maxVal, minLoc, maxLoc) = cv2.minMaxLoc(mask)
#
#     cv2.circle(frame, maxLoc, 20, (0, 0, 255), 2, cv2.LINE_AA)
#     cv2.imshow('Track Laser', frame)
#
#     if cv2.waitKey(1) & 0xFF == ord('q'):
#         break
#
# cap.release()
# cv2.destroyAllWindows()
#
# import cv2
# import numpy as np
#
# cap = cv2.VideoCapture(0)
#
# azulBajo = np.array([145, 100, 20], np.uint8)
# azulAlto = np.array([155, 255, 255], np.uint8)
#
# while True:
#
#     ret, frame = cap.read()
#
#     if ret == True:
#         frameHSV = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
#         mask = cv2.inRange(frameHSV, azulBajo, azulAlto)
#
#         cv2.imshow('maskAzul', mask)
#         cv2.imshow('frame', frame)
#         if cv2.waitKey(1) & 0xFF == ord('s'):
#             break
# cap.release()
# cv2.destroyAllWindows()

#
#


import cv2
import numpy as np

cap = cv2.VideoCapture(0)

azulBajo = np.array([175,100,20],np.uint8)
azulAlto = np.array([179,255,255],np.uint8)
while True:

  ret,frame = cap.read()

  if ret==True:
    frameHSV = cv2.cvtColor(frame,cv2.COLOR_BGR2HSV)
    mask = cv2.inRange(frameHSV,azulBajo,azulAlto)
    contornos,_ = cv2.findContours(mask, cv2.RETR_EXTERNAL,
      cv2.CHAIN_APPROX_SIMPLE)
    #cv2.drawContours(frame, contornos, -1, (255,0,0), 3)
    for c in contornos:
      area = cv2.contourArea(c)
      if area > 3000:
        M = cv2.moments(c)
        if (M["m00"]==0): M["m00"]=1
        x = int(M["m10"]/M["m00"])
        y = int(M['m01']/M['m00'])
        cv2.circle(frame, (x,y), 7, (0,255,0), -1)
        font = cv2.FONT_HERSHEY_SIMPLEX
        cv2.putText(frame, '{},{}'.format(x,y),(x+10,y), font, 0.75,(0,255,0),1,cv2.LINE_AA)
        nuevoContorno = cv2.convexHull(c)
        cv2.drawContours(frame, [nuevoContorno], 0, (255,0,0), 3)
    #cv2.imshow('maskAzul',mask)
    cv2.imshow('frame',frame)
    if cv2.waitKey(1) & 0xFF == ord('s'):
      break
cap.release()
cv2.destroyAllWindows()







# import cv2
# import numpy as np
#
# cap = cv2.VideoCapture(0)
#
# redBajo1 = np.array([0, 100, 20], np.uint8)
# redAlto1 = np.array([8, 255, 255], np.uint8)
#
# redBajo2=np.array([175, 100, 20], np.uint8)
# redAlto2=np.array([179, 255, 255], np.uint8)
#
# while True:
#   ret,frame = cap.read()
#   if ret==True:
#     frameHSV = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
#     maskRed1 = cv2.inRange(frameHSV, redBajo1, redAlto1)
#     maskRed2 = cv2.inRange(frameHSV, redBajo2, redAlto2)
#     maskRed = cv2.add(maskRed1, maskRed2)
#     maskRedvis = cv2.bitwise_and(frame, frame, mask= maskRed)
#     cv2.imshow('frame', frame)
#     cv2.imshow('maskRed', maskRed)
#     cv2.imshow('maskRedvis', maskRedvis)
#     if cv2.waitKey(1) & 0xFF == ord('s'):
#       break
# cap.release()
# cv2.destroyAllWindows()