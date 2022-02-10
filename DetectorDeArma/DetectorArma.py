# import cv2
# import mediapipe as mp
# import numpy as np
# import pyautogui
#
# mp_drawing = mp.solutions.drawing_utils
# mp_hands = mp.solutions.hands
# cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
#
# color_mouse_pointer = (255, 0, 255)
#

#
#
# def calculate_distance(x1, y1, x2, y2):
#     p1 = np.array([x1, y1])
#     p2 = np.array([x2, y2])
#     return np.linalg.norm(p1 - p2)
# def detect_finger_down(hand_landmarks):
#     finger_down = False
#     color_base = (255, 0, 112)
#     color_index = (255, 198, 82)
#     x_base1 = int(hand_landmarks.landmark[0].x * width)
#     y_base1 = int(hand_landmarks.landmark[0].y * height)
#     x_base2 = int(hand_landmarks.landmark[9].x * width)
#     y_base2 = int(hand_landmarks.landmark[9].y * height)
#     x_index = int(hand_landmarks.landmark[8].x * width)
#     y_index = int(hand_landmarks.landmark[8].y * height)
#     d_base = calculate_distance(x_base1, y_base1, x_base2, y_base2)
#     d_base_index = calculate_distance(x_base1, y_base1, x_index, y_index)
#     if d_base_index < d_base:
#         finger_down = True
#         color_base = (255, 0, 255)
#         color_index = (255, 0, 255)
#     cv2.circle(frame, (x_base1, y_base1), 5, color_base, 2)
#     cv2.circle(frame, (x_index, y_index), 5, color_index, 2)
#     cv2.line(frame, (x_base1, y_base1), (x_base2, y_base2), color_base, 3)
#     cv2.line(frame, (x_base1, y_base1), (x_index, y_index), color_index, 3)
#     return finger_down
#
#
#
# with mp_hands.Hands(
#         static_image_mode=False,
#         max_num_hands=1,
#         min_detection_confidence=0.5) as hands:
#     while True:
#         ret, frame = cap.read()
#         if ret == False:
#             break
#
#         height, width, _ = frame.shape
#         frame = cv2.flip(frame, 1)
#
#         frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
#
#         results = hands.process(frame_rgb)
#
#         if results.multi_hand_landmarks is not None:
#             for hand_landmarks in results.multi_hand_landmarks:
#                 x = int(hand_landmarks.landmark[9].x * width)
#                 xx=round((x/10),1)
#
#                 y = int(hand_landmarks.landmark[9].y * height)
#                 yy = round((y / 10), 1)
#                 # print(yy)
#                 if detect_finger_down(hand_landmarks):
#                     pyautogui.click()
#                     print("se pisparo")
#                 else:
#                     print("no disparo")
#                 cv2.circle(frame, (x, y), 10, color_mouse_pointer, 3)
#                 cv2.circle(frame, (x, y), 5, color_mouse_pointer, -1)
#
#                 ax = round(map(xx, 2, 62, -28, 32), 1)
#                 ay = round(map(yy, 40, 10, 8, -10), 1)
#                 rx = str(ax)
#                 ry = str(ay)
#             try:
#                 d = open('DatosPuntero.json', 'w')
#                 d.write(rx + ';' + ry)
#                 d.close()
#             except:
#                 pass
#
#             cv2.putText(frame, f'x: {rx}', (20, 50), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
#             cv2.putText(frame, f'y: {ry}', (20, 80), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
#
#         cv2.imshow('Frame', frame)
#         if cv2.waitKey(1) & 0xFF == 27:
#             break
# cap.release()
# cv2.destroyAllWindows()




import cv2
import mediapipe as mp
import numpy as np
import pyautogui
import time

mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands
cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
rx = 0.0
ry = 0.0
xa=0

def map (longx, in_min, in_max, out_min, out_max):
    return (longx - in_min) * (out_max - out_min) / (in_max - in_min) + out_min

with mp_hands.Hands(
    static_image_mode=False,
    max_num_hands=1,
    min_detection_confidence=0.5) as hands:
    while True:
        ret, frame = cap.read()
        start = time.time()
        if ret == False:
            break
        height, width, _ = frame.shape
        frame = cv2.flip(frame, 1)
        frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        results = hands.process(frame_rgb)
        if results.multi_hand_landmarks is not None:
            for hand_landmarks in results.multi_hand_landmarks:
                X = int(hand_landmarks.landmark[10].x * width)
                y = int(hand_landmarks.landmark[10].y * height)
                a = X
                b = y - 30
                xx = round((a / 10), 1)
                yy = round((b / 10), 1)
                # print(a)
                # if a < 310:
                #     a=a-10
                # if a > 310:
                #     a=a+10

                cv2.circle(frame, (a, b), 3, (255, 0, 0), 3)
                cv2.circle(frame, (a, b), 3, (255, 255, 255), -1)
                ax = round(map(a, 90, 500, -20, 20), 1)
                ay = round(map(b, 470, 70, 10, -10), 1)
                rx = str(ax)
                ry = str(ay)
            try:
                d = open('DatosPuntero.json', 'w')
                d.write(rx + ';' + ry)
                d.close()
            except:
                pass
        end = time.time()
        totaltime = end - start
        fps = 1 / totaltime
        cv2.putText(frame, f'FPS: {int(fps)}', (20, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
        cv2.putText(frame, f'x: {rx}', (20, 50), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
        cv2.putText(frame, f'y: {ry}', (20, 80), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
        cv2.imshow('Frame', frame)
        if cv2.waitKey(1) & 0xFF == 27:
            break
cap.release()
cv2.destroyAllWindows()

#
# import cv2
# import mediapipe as mp
#
# mp_drawing = mp.solutions.drawing_utils
# mp_holistic = mp.solutions.holistic
#
# # cap = cv2.VideoCapture("video_0001.mp4")
# cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
# color_mouse_pointer = (255, 0, 255)
# with mp_holistic.Holistic(
#         static_image_mode=False,
#         model_complexity=1) as holistic:
#     while True:
#         ret, frame = cap.read()
#         if ret == False:
#             break
#
#         height, width, _ = frame.shape
#         frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
#         results = holistic.process(frame_rgb)
#
#         # Mano izquieda (azul)
#         # if results.left_face_landmarks is not None:
#         #     for results.left_hand_landmarks in results.multi_face_landmarks:
#         x = int(results.left_hand_landmarks.landmark[9].x * width)
#
#
#         y = int(results.left_hand_landmarks.landmark[9].y * height)
#
#         cv2.circle(frame, (x, y), 10, color_mouse_pointer, 3)
#         cv2.circle(frame, (x, y), 5, color_mouse_pointer, -1)
#
#
#
#         # mp_drawing.draw_landmarks(
#         #     frame, results.left_hand_landmarks, mp_holistic.HAND_CONNECTIONS,
#         #     mp_drawing.DrawingSpec(color=(255, 255, 0), thickness=2, circle_radius=1),
#         #     mp_drawing.DrawingSpec(color=(255, 0, 0), thickness=2))
#
#         # Mano derecha (verde)
#         mp_drawing.draw_landmarks(
#             frame, results.right_hand_landmarks, mp_holistic.HAND_CONNECTIONS,
#             mp_drawing.DrawingSpec(color=(0, 255, 0), thickness=2, circle_radius=1),
#             mp_drawing.DrawingSpec(color=(57, 143, 0), thickness=2))
#
#         frame = cv2.flip(frame, 1)
#         cv2.imshow("Frame", frame)
#         if cv2.waitKey(1) & 0xFF == 27:
#             break
#
# cap.release()
# cv2.destroyAllWindows()