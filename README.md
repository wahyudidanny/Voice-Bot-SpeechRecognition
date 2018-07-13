# Voice-Bot-SpeechRecognition
C# Speech Recognition Bot

# There are some requirement you need to have, in order to execute the project successfully, such as:

1.Hardware = Microphone (as long the microphone still working, any kind of microphone will do)

  I'm operated this bot in windows 7
  
  	Go to ControlPanel >> Hardware and Sound >> Manage audio device >> tab Recording 
		
  In tab recording, you can check if your microphone still working or not

-------------------------------------------------------------------------------------------------------------------------
	
2.Speech Recognition (Optional)

	Go to ControlPanel >> Hardware and Sound >> Manage audio device >> tab Recording >> Right-Click Microphone 
	>> Configure Speech
  
In order to execute some task you command, first the computer need to recognize your voice 
to know what your intention are. By improving your vocabulary, speech, grammar and dictation, 
the computer will understand your command much better and faster. 

You can find out more about speech recognition in link below

https://en.wikipedia.org/wiki/Windows_Speech_Recognition
	
-------------------------------------------------------------------------------------------------------------------------
	
3.Reference >> System.Speech 

	Go to your Project >> Right-Click Reference >> Add Reference >> type Speech (System.Speech) >> Mark it and OK 
  
The reference in visual studio, so you can begin your Bot project

-------------------------------------------------------------------------------------------------------------------------

NOTE: In voice command folder, there are 2 file txt: 

	1.commandfullgrammar.txt (not recomended)
	2.commands.txt  
				
				
I prefer the (commands.txt) because bot only recognize what's in the file (else didn't do anything) as you speak,
rather than the first tx. If it full grammar, the bot can have a couple mistake because of the similar pronoun 
and dictation, such as rabbit = rabid, flower = flour, to = two = too , and etc.

-------------------------------------------------------------------------------------------------------------------------

Right now the speech recognition bot have multiple feature, such as :

	1. Search google
	2. Open Firefox
	3. Open Spotify (Play, Stop and Close)
	4. 9GAG (Favourite Apps)
	5. Maximize, minimize, unminimize program
	6. Jokes
	7. Weather 
	8. Etc

This is still early development. If you have an idea and feature, feel free to make change in this repository

Thank You
