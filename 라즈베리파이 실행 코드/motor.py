import RPi.GPIO as GPIO
from time import sleep

GPIO.setmode(GPIO.BOARD)
GPIO.setup(31, GPIO.OUT)

motor = GPIO.PWM(31,20)
motor.start(0)



motor.ChangeDutyCycle(1)
sleep(1)
motor.ChangeDutyCycle(6)
sleep(1)

GPIO.cleanup()
