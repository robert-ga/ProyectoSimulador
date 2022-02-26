# import cv2
# import mediapipe as mp
#
# mp_drawing = mp.solutions.drawing_utils
# mp_pose = mp.solutions.pose
#
# #cap = cv2.VideoCapture("video_0002.mp4")
# cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
#
# with mp_pose.Pose(
#     static_image_mode=False) as pose:
#
#     while True:
#         ret, frame = cap.read()
#         if ret == False:
#             break
#         frame = cv2.flip(frame, 1)
#         height, width, _ = frame.shape
#         frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
#         results = pose.process(frame_rgb)
#
#         if results.pose_landmarks is not None:
#
#             x = int(results.pose_landmarks.landmark[20].x * width)
#             y = int(results.pose_landmarks.landmark[20].y * height)
#             x1 = int(results.pose_landmarks.landmark[19].x * width)
#             y1 = int(results.pose_landmarks.landmark[19].y * height)
#             xx = int((x + x1) / 2)
#             yy = int((y + y1) / 2)
#
#             cv2.circle(frame, (x, y), 5, (255, 0, 0), 3)
#             cv2.circle(frame, (x, y), 5, (255, 255, 255), -1)
#             cv2.circle(frame, (x1, y1), 5, (255, 0, 0), 3)
#             cv2.circle(frame, (x1, y1), 5, (255, 255, 255), -1)
#             cv2.circle(frame, (xx, yy), 5, (255, 0, 0), 3)
#             cv2.circle(frame, (xx, yy), 5, (255, 0, 255), -1)
#             # mp_drawing.draw_landmarks(
#             #     frame, results.pose_landmarks, mp_pose.POSE_CONNECTIONS,
#             #     mp_drawing.DrawingSpec(color=(128, 0, 250), thickness=2, circle_radius=3),
#             #     mp_drawing.DrawingSpec(color=(255, 255, 255), thickness=2))
#
#         cv2.imshow("Frame", frame)
#         if cv2.waitKey(1) & 0xFF == 27:
#             break
#
# cap.release()
# cv2.destroyAllWindows()


# import cv2
# import numpy as np
# import pyautogui
#
# while True:
#      screenshot = pyautogui.screenshot(region=(0, 0, 1920, 1080))
#      screenshot = np.array(screenshot)
#      screenshot = cv2.cvtColor(screenshot, cv2.COLOR_RGB2BGR)
#      cv2.imshow("screenshot", screenshot)
#      if cv2.waitKey(1) & 0xFF == 27:
#           break
# cv2.destroyAllWindows()

import cv2
import mediapipe as mp
import numpy as np
import pyautogui

mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands
cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)

color_mouse_pointer = (255, 0, 255)

# Puntos de la pantalla-juego
SCREEN_GAME_X_INI = 0
SCREEN_GAME_Y_INI = 0
SCREEN_GAME_X_FIN = 0 + 1920
SCREEN_GAME_Y_FIN = 0 + 1080

aspect_ratio_screen = (SCREEN_GAME_X_FIN - SCREEN_GAME_X_INI) / (SCREEN_GAME_Y_FIN - SCREEN_GAME_Y_INI)
print("aspect_ratio_screen:", aspect_ratio_screen)

X_Y_INI = 10
with mp_hands.Hands(
        static_image_mode=False,
        max_num_hands=1,
        min_detection_confidence=0.5) as hands:
     while True:
          ret, frame = cap.read()
          if ret == False:
               break

          height, width, _ = frame.shape
          frame = cv2.flip(frame, 1)

          # Dibujando un área proporcional a la del juego
          area_width = width - X_Y_INI * 2
          area_height = int(area_width-160)

          aux_image = np.zeros(frame.shape, np.uint8)
          aux_image = cv2.rectangle(aux_image, (X_Y_INI, X_Y_INI), (X_Y_INI + area_width, X_Y_INI + area_height),
                                    (255, 0, 0), -1)
          output = cv2.addWeighted(frame, 1, aux_image, 0.7, 0)

          frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

          results = hands.process(frame_rgb)

          if results.multi_hand_landmarks is not None:
               for hand_landmarks in results.multi_hand_landmarks:
                    x = int(hand_landmarks.landmark[9].x * width)
                    y = int(hand_landmarks.landmark[9].y * height)


                    xm = np.interp(x, (X_Y_INI, X_Y_INI + area_width), (SCREEN_GAME_X_INI, SCREEN_GAME_X_FIN))
                    ym = np.interp(y, (X_Y_INI, X_Y_INI + area_height), (SCREEN_GAME_Y_INI, SCREEN_GAME_Y_FIN))
                    pyautogui.moveTo(int(xm), int(ym))
                    cv2.circle(output, (x, y), 10, color_mouse_pointer, 3)
                    cv2.circle(output, (x, y), 5, color_mouse_pointer, -1)

          cv2.imshow('output', output)
          if cv2.waitKey(1) & 0xFF == 27:
               break
cap.release()
cv2.destroyAllWindows()