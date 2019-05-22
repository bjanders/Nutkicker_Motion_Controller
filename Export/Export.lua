local nmc_file = nil 
local data_file = nil 

function LuaExportStart()

	nmc_file = io.open("C:/Users/frank/Saved Games/DCS/Logs/Recorder.nmc", "w") 
	data_file = io.open("C:/Users/frank/Saved Games/DCS/Logs/Data.txt", "w") 
                      
	--**********************
	--**  Nutkicker Code  **
	--**********************
	package.path  = package.path..";"..lfs.currentdir().."/LuaSocket/?.lua"
	package.cpath = package.cpath..";"..lfs.currentdir().."/LuaSocket/?.dll"

	socket = require("socket")
	IPAddress = "127.0.0.1" -- localhost
	--IPAddress = "192.168.178.63" -- Dedicated Laptop
	--IPAddress = "192.168.178.33" -- Simulator
	--IPAddress = "192.168.178.56" -- MacBook Pro
	PortS = 31090

	Nutkicker_socket = socket.try(socket.connect(IPAddress, PortS))
	Nutkicker_socket:setoption("tcp-nodelay",true) 

	NMC_Counter = 0
	--*********************
	--**  Nutkicker END  **
	--*********************
end

function LuaExportBeforeNextFrame()
end

function LuaExportAfterNextFrame()
	--**********************
	--**  Nutkicker Code  **
	--**********************
	
	--Airdata:
		local NMC_IAS = LoGetIndicatedAirSpeed()
		local NMC_Machnumber = LoGetMachNumber()
		local NMC_TAS = LoGetTrueAirSpeed()
		local vv = LoGetVectorVelocity()
		local NMC_GS = math.sqrt( math.pow(vv.x,2) + math.pow(vv.z,2))
		local NMC_AOA = LoGetAngleOfAttack();
		local NMC_VerticalSpeed = LoGetVerticalVelocity()
		local NMC_Height = LoGetAltitudeAboveGroundLevel()

	--Euler angles:
		local mySelf = LoGetSelfData()
		--local ADI_pitch, ADI_bank, ADI_yaw = LoGetADIPitchBankYaw()
		local NMC_inertial_Yaw = 	mySelf.Heading
		local NMC_inertial_Pitch = 	mySelf.Pitch
		local NMC_inertial_Bank = 	mySelf.Bank

	--Angular rates:
		local NMC_Omega = LoGetAngularVelocity()

	--Accelerations:
		local NMC_Accel = LoGetAccelerationUnits()

	--Metadata:
		local NMC_Time = LoGetModelTime()
		NMC_Counter = NMC_Counter + 1
	
	--Here the data is being sent to the Nutkicker Software: 
	socket.try(Nutkicker_socket:send(string.format("%.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f, %.4f  \n",		
													NMC_IAS,					--Airdata[0]
													NMC_Machnumber,				--Airdata[1]
													NMC_TAS,					--Airdata[2]
													NMC_GS,						--Airdata[3]
													NMC_AOA,					--Airdata[4]
													NMC_VerticalSpeed,			--Airdata[5]
													NMC_Height,					--Airdata[6]
													NMC_inertial_Bank,			--Euler[0]
													NMC_inertial_Yaw,			--Euler[1]
													NMC_inertial_Pitch,			--Euler[2]
													NMC_Omega.x,				--Rates[0]
													NMC_Omega.y,				--Rates[1]
													NMC_Omega.z,				--Rates[2]
													NMC_Accel.x,				--Accel[0]
													NMC_Accel.y,				--Accel[1]
													NMC_Accel.z,				--Accel[2]
													NMC_Time,					--Meta[0]
													NMC_Counter					--Meta[1]
																							)))
	--*********************
	--**  Nutkicker END  **
	--*********************
	
	
	nmc_file:write(string.format("%.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f\t %.4f \n",		
													NMC_IAS,					--Airdata[0]
													NMC_Machnumber,				--Airdata[1]
													NMC_TAS,					--Airdata[2]
													NMC_GS,						--Airdata[3]
													NMC_AOA,					--Airdata[4]
													NMC_VerticalSpeed,			--Airdata[5]
													NMC_Height,					--Airdata[6]
													NMC_inertial_Bank,			--Euler[0]
													NMC_inertial_Yaw,			--Euler[1]
													NMC_inertial_Pitch,			--Euler[2]
													NMC_Omega.x,				--Rates[0]
													NMC_Omega.y,				--Rates[1]
													NMC_Omega.z,				--Rates[2]
													NMC_Accel.x,				--Accel[0]
													NMC_Accel.y,				--Accel[1]
													NMC_Accel.z,				--Accel[2]
													NMC_Time,					--Meta[0]
													NMC_Counter					--Meta[1]
																							))
end

function LuaExportStop()

	--**********************
	--**  Nutkicker Code  **
	--**********************
	if Nutkicker_socket then 
		socket.try(Nutkicker_socket:send("exit")) -- closing the socket
		Nutkicker_socket:close()
	end
	--*********************
	--**  Nutkicker END  **
	--*********************
end

function LuaExportActivityNextEvent(t)
	local tNext = t
	
	data_file:write(string.format("%.4f \n", LoGetAccelerationUnits().x))
	
	tNext = tNext + 0.1
	return tNext

end




--local t = LoGetModelTime()
--local altRad = LoGetAltitudeAboveGroundLevel()
--local pitch, bank, yaw = LoGetADIPitchBankYaw()
--local glide = LoGetGlideDeviation()
--local side = LoGetSideDeviation()
--local slip = LoGetSlipBallPosition()
--local gs = LoGetAccelerationUnits()
--local navinfo = LoGetNavigationInfo()
--local vertvel = LoGetVerticalVelocity()
--local trueairspeed = LoGetTrueAirSpeed()
--local indicatedairspeed = LoGetIndicatedAirSpeed()
--local machnumber = LoGetMachNumber()
--local angleofattack = LoGetAngleOfAttack()
--local MyPlane = LoGetSelfData()
--local LatPos = MyPlane.LatLongAlt.Lat
--local LongPos = MyPlane.LatLongAlt.Long
--local plane = MyPlane.Name
--local mech = LoGetMechInfo()
--local eleron = mech.controlsurfaces.eleron.right
--local relevator = mech.controlsurfaces.elevator.right
--local lelevator = mech.controlsurfaces.elevator.left
--local rrudder = mech.controlsurfaces.rudder.right
--local lrudder = mech.controlsurfaces.rudder.left

--socket.try(c:send(string.format("plane = %s, bank = %.4f, pitch = %.4f, t = %.4f, yaw = %.4f, slip = %f, y = %f, x = %f, relevator = %f, lelevator = %f, eleron = %f, rudder = %f, trueairspeed = %f, angleofattack = %f\n", plane, bank, pitch, t, yaw, slip, gs.y, av.x, relevator, lelevator, eleron, rudder, trueairspeed, angleofattack)))

