import time
import cv2
import mediapipe as mp
import json

mp_face_mesh = mp.solutions.face_mesh
mp_drawing = mp.solutions.drawing_utils
handsModule = mp.solutions.hands
cap = cv2.VideoCapture(0, cv2.CAP_DSHOW)
rx = 0.0
ry = 0.0
rz = 0.0
P = False

def map (longx, in_min, in_max, out_min, out_max):
    return (longx - in_min) * (out_max - out_min) / (in_max - in_min) + out_min

class Tests():
    def Rostro(self):
        P = True
        return P

with mp_face_mesh.FaceMesh(
        static_image_mode=False,
        max_num_faces=1,
        min_detection_confidence=0.5) as face_mesh:
    while Tests:
        ret, frame = cap.read()
        start = time.time()
        if ret == False:
            break
        frame = cv2.flip(frame, 1)
        frame_rgb = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)
        results = face_mesh.process(frame_rgb)
        if results.multi_face_landmarks is not None:
            for face_landmarks in results.multi_face_landmarks:
                for datos in handsModule.HandLandmark:
                    # mp_drawing.draw_landmarks(frame, face_landmarks,
                    # mp_face_mesh.FACEMESH_CONTOURS,
                    # mp_drawing.DrawingSpec(color=(0, 0, 0), thickness=1, circle_radius=1),
                    # mp_drawing.DrawingSpec(color=(255, 0, 0), thickness=1))
                    normalizedLandmark = face_landmarks.landmark[datos]
                    X = normalizedLandmark.x * 10
                    y = normalizedLandmark.y * 10
                    z = normalizedLandmark.z * 10
                    # Rec Final
                    ax = round(map(X, 2.2, 7.5, -13.36, 6), 1)
                    ay = round(map(y, 8, 2, 2.8, 6), 1)
                    az = round(map(z, -0.8, -0.3, 13, -5.1), 1)
                    # otro
                    # ax = round(map(X, 2.2, 7.5, -16, 8), 1)
                    # ay = round(map(y, 8, 2, 2.8, 6), 1)
                    # az = round(map(z, -0.8, -0.3, 13, -5.1), 1)
                    # pc
                    # ax = round(map(X, 2.2, 7.5, -9.6, 2.5), 1)
                    # ay = round(map(y, 8, 2, 2.8, 6), 1)
                    # az = round(map(z, -0.8, -0.3, 10, 1), 1)
                    # tv casa
                    # ax = round(map(X, 2, 7, -7, 2), 1)
                    # ay = round(map(y, 8.5, 3.2, 2.8, 6), 1)
                    # az = round(map(z, -0.8, -0.3, 13, -5.1), 1)
                    # tv emi
                    # ax = round(map(X, 1.4, 8.8, -9.6, 2.68), 1)
                    # ay = round(map(y, 3.8, 8.1, 2.8, 6), 1)
                    # az = round(map(z, -0.20, -0.11, 10, 1), 1)

                    rx = str(ax)
                    ry = str(ay)
                    rz = str(az)
            try:
                d = open('DatosSalida.json', 'w')
                d.write(rx + ';' + ry + ';' + rz)
                d.close()
            except:
                pass

        end = time.time()
        totaltime = end - start
        fps = 1 / totaltime
        cv2.putText(frame, f'FPS: {int(fps)}', (20, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
        cv2.putText(frame, f'x: {rx}', (20, 50), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
        cv2.putText(frame, f'y: {ry}', (20, 80), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
        cv2.putText(frame, f'z: {rz}', (20, 120), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1)
        cv2.imshow("Detector", frame)
        # cv2.imwrite("Rostro1.png", frame)
        if cv2.waitKey(1) & 0xFF == ord('q'):
            break
        # if k == 27:
        #     break
cap.release()
cv2.destroyAllWindows()
