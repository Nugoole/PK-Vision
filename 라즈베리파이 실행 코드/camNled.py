import time
import board
import neopixel
import io
import socket
import sys




#led parameters
pixel_pin = board.D18
num_pixels = 1
ORDER = neopixel.GRB
pixels = neopixel.NeoPixel(pixel_pin, num_pixels, brightness = 0.7, auto_write = False,pixel_order = ORDER)

		
#pixels.brightness = 1;	

import picamera

count = 0

with picamera.PiCamera() as camera:
	
	
	pixels.fill((0,0,0))
	pixels.show()
	
	#camera.brightness = 47
	#camera.resolution = (1920,1080)
	camera.resolution = (1280,960)
	camera.shutter_speed = 1500
	#camera.start_preview()
	
	
		
	
	
	#camera.resolution = (1024, 768)
	#camera.awb_mode = 'fluorescent'
	#camera.shutter_speed = 50000
	
	pixels.fill((255,255,255))
	pixels.show()
	time.sleep(0.5)
	camera.capture('/home/pi/' + sys.argv[1])
	#camera.stop_preview()
	pixels.fill((0,0,0))
	pixels.show()
	
	
	
	
	
	
	
