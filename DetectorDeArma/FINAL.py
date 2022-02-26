import cv2
import numpy as np
import pyautogui
cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
font = cv2.FONT_HERSHEY_COMPLEX
Kernal = np.ones((3, 3), np.uint8)
color_mouse_pointer = (255, 0, 0)
SCREEN_GAME_X_INI = 0
SCREEN_GAME_Y_INI = 0
SCREEN_GAME_X_FIN = 0 + 1920
SCREEN_GAME_Y_FIN = 0 + 1080
aspect_ratio_screen = (SCREEN_GAME_X_FIN - SCREEN_GAME_X_INI) / (SCREEN_GAME_Y_FIN - SCREEN_GAME_Y_INI)
print("aspect_ratio_screen:", aspect_ratio_screen)
X_Y_INI = 100
while(True):
    ret, frame = cap.read()
    if ret == False:
        break
    height, width, _ = frame.shape
    frame = cv2.flip(frame, 1)
    area_width = width - X_Y_INI * 2
    area_height = int(area_width / aspect_ratio_screen)
    aux_image = np.zeros(frame.shape, np.uint8)
    aux_image = cv2.rectangle(aux_image, (X_Y_INI, X_Y_INI), (X_Y_INI + area_width, X_Y_INI + area_height),
                              (255, 255, 255), -1)
    output = cv2.addWeighted(frame, 1, aux_image, 0.7, 0)
    frame2 = cv2.cvtColor(frame, cv2.COLOR_BGR2HSV)
    lb = np.array([175, 119, 212])
    ub = np.array([255, 255, 255])
    mask = cv2.inRange(frame2, lb, ub)
    opening = cv2.morphologyEx(mask, cv2.MORPH_OPEN, Kernal)
    res = cv2.bitwise_and(frame, frame, mask= opening)
    contours, hierarchy = cv2.findContours(opening, cv2.RETR_TREE,
                                           cv2.CHAIN_APPROX_NONE)
    if len(contours) != 0:
        cnt = contours[0]
        area = cv2.contourArea(cnt)
        M = cv2.moments(cnt)
        x = int(M['m10']/M['m00'])
        y = int(M['m01'] / M['m00'])
        xm = np.interp(x, (X_Y_INI, X_Y_INI + area_width), (SCREEN_GAME_X_INI, SCREEN_GAME_X_FIN))
        ym = np.interp(y, (X_Y_INI, X_Y_INI + area_height), (SCREEN_GAME_Y_INI, SCREEN_GAME_Y_FIN))
        pyautogui.moveTo(int(xm), int(ym))
        cv2.circle(output, (x, y), 10, color_mouse_pointer, 3)
        cv2.circle(output, (x, y), 5, color_mouse_pointer, -1)
    if cv2.waitKey(1) == ord('s'):
        break
    cv2.imshow('Imagen', output)
cap.release()
cv2.destroyAllWindows()