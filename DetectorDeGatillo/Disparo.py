import cv2
import mediapipe as mp
import numpy as np
import pyautogui

mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands

cap = cv2.VideoCapture(1, cv2.CAP_DSHOW)


def calculate_distance(x1, y1, x2, y2):
    p1 = np.array([x1, y1])
    p2 = np.array([x2, y2])
    return np.linalg.norm(p1 - p2)

def detect_finger_down(hand_landmarks):
    finger_down = False
    color_base = (255, 0, 112)
    color_index = (255, 198, 82)
    x_base1 = int(hand_landmarks.landmark[5].x * width)
    y_base1 = int(hand_landmarks.landmark[5].y * height)

    x_base2 = int(hand_landmarks.landmark[7].x * width)
    y_base2 = int(hand_landmarks.landmark[7].y * height)

    x_index = int(hand_landmarks.landmark[8].x * width)
    y_index = int(hand_landmarks.landmark[8].y * height)

    d_base = calculate_distance(x_base1, y_base1, x_base2, y_base2)
    d_base_index = calculate_distance(x_base1, y_base1, x_index, y_index)
    if d_base_index < d_base:
        finger_down = True
        color_base = (255, 0, 0)
        color_index = (255, 0, 0)
    cv2.circle(frame, (x_base1, y_base1), 5, color_base, 2)
    cv2.circle(frame, (x_index, y_index), 5, color_index, 2)
    cv2.line(frame, (x_base1, y_base1), (x_base2, y_base2), color_base, 3)
    cv2.line(frame, (x_base1, y_base1), (x_index, y_index), color_index, 3)
    return finger_down

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
        frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

        results = hands.process(frame_rgb)

        if results.multi_hand_landmarks is not None:
            for hand_landmarks in results.multi_hand_landmarks:
                # x1=int(hand_landmarks.landmark[0].x*width)
                # y1 = int(hand_landmarks.landmark[0].y * height)
                #
                # x2 = int(hand_landmarks.landmark[4].x * width)
                # y2 = int(hand_landmarks.landmark[4].y * height)

                x3 = int(hand_landmarks.landmark[8].x * width)
                y3 = int(hand_landmarks.landmark[8].y * height)

                x4 = int(hand_landmarks.landmark[7].x * width)
                y4 = int(hand_landmarks.landmark[7].y * height)
                #
                # x5 = int(hand_landmarks.landmark[16].x * width)
                # y5 = int(hand_landmarks.landmark[16].y * height)
                #
                # x6 = int(hand_landmarks.landmark[20].x * width)
                # y6 = int(hand_landmarks.landmark[20].y * height)
                if detect_finger_down(hand_landmarks):
                    pyautogui.click()
                    print("se pisparo")
                else:
                    print("no disparo")

                # cv2.circle(frame,(x1,y1),3,(255,0,0),3)
                # cv2.circle(frame, (x1, y1), 3, (255, 255, 255), -1)
                #
                # cv2.circle(frame, (x2, y2), 3, (255, 0, 0), 3)
                # cv2.circle(frame, (x2, y2), 3, (255, 255, 255), -1)

                cv2.circle(frame, (x3, y3), 3, (255, 0, 0), 3)
                cv2.circle(frame, (x3, y3), 3, (255, 255, 255), -1)

                cv2.circle(frame, (x4, y4), 3, (255, 0, 0), 3)
                cv2.circle(frame, (x4, y4), 3, (255, 255, 255), -1)
                #
                # cv2.circle(frame, (x5, y5), 10, (255, 0, 0), 1)
                # cv2.circle(frame, (x5, y5), 5, (255, 0, 0), -1)
                #
                # cv2.circle(frame, (x6, y6), 10, (255, 0, 0), 1)
                # cv2.circle(frame, (x6, y6), 5, (255, 0, 0), -1)
                # mp_drawing.draw_landmarks(
                #     frame, hand_landmarks, mp_hands.HAND_CONNECTIONS,
                #     mp_drawing.DrawingSpec(color=(255, 0, 0), thickness=3, circle_radius=5),
                #     mp_drawing.DrawingSpec(color=(255, 255, 255), thickness=4, circle_radius=5))

        cv2.imshow('Frame', frame)
        if cv2.waitKey(1) & 0xFF == 27:
            break
cap.release()
cv2.destroyAllWindows()