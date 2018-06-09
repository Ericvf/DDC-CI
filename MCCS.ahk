; https://autohotkey.com/board/topic/96884-change-monitor-input-source/page-2

; Finds monitor handle based on MousePosition
getMonitorHandle()
{
  MouseGetPos, xpos, ypos
  point := ( ( xpos ) & 0xFFFFFFFF ) | ( ( ypos ) << 32 )
  ; Initialize Monitor handle
  hMon := DllCall("MonitorFromPoint"
    , "int64", point ; point on monitor
    , "uint", 1) ; flag to return primary monitor on failure

    
  ; Get Physical Monitor from handle
  VarSetCapacity(Physical_Monitor, 8 + 256, 0)

  DllCall("dxva2\GetPhysicalMonitorsFromHMONITOR"
    , "int", hMon   ; monitor handle
    , "uint", 1   ; monitor array size
    , "int", &Physical_Monitor)   ; point to array with monitor

  return hPhysMon := NumGet(Physical_Monitor)
}

destroyMonitorHandle(handle)
{
  DllCall("dxva2\DestroyPhysicalMonitor", "int", handle)
}

; Used to change the monitor source
; DVI = 3
; HDMI = 4
; YPbPr = 12
setMonitorInputSource(source)
{
  handle := getMonitorHandle()
  DllCall("dxva2\SetVCPFeature"
    , "int", handle
    , "char", 0x60 ;VCP code for Input Source Select
    , "uint", source)
  destroyMonitorHandle(handle)
}

; Gets Monitor source
getMonitorInputSource()
{
  handle := getMonitorHandle()
  DllCall("dxva2\GetVCPFeatureAndVCPFeatureReply"
    , "int", handle
    , "char", 0x60 ;VCP code for Input Source Select
    , "Ptr", 0
    , "uint*", currentValue
    , "uint*", maximumValue)
  destroyMonitorHandle(handle)
  return currentValue
}

; Gets Monitor source
getMonitorVolume()
{
  handle := getMonitorHandle()
  DllCall("dxva2\GetVCPFeatureAndVCPFeatureReply"
    , "int", handle
    , "char", 0x62 ;VCP code for Input Source Select
    , "Ptr", 0
    , "uint*", currentValue
    , "uint*", maximumValue)
  destroyMonitorHandle(handle)
  return currentValue
}


; Set volume 
setMonitorVolume(source)
{
  handle := getMonitorHandle()
  DllCall("dxva2\SetVCPFeature"
    , "int", handle
    , "char", 0x62 ;VCP code for Input Source Select
    , "uint", source)
  destroyMonitorHandle(handle)
}


; Set volume 
setMonitorBrightness(source)
{
  handle := getMonitorHandle()
  DllCall("dxva2\SetVCPFeature"
    , "int", handle
    , "char", 0x10 ;VCP code for Input Source Select
    , "uint", source)
  destroyMonitorHandle(handle)
}


; Set volume 
getMonitorCapabilities()
{
  handle := getMonitorHandle()
  DllCall("dxva2\GetCapabilitiesStringLength"
    , "int", handle
    , "uint*", lengthValue)

  MsgBox, % lengthValue

 VarSetCapacity(Buf,lengthValue * 2, 0)
  DllCall("dxva2\CapabilitiesRequestAndCapabilitiesReply"
    , "int", handle
    , "Str", Buf
    , "uint", lengthValue)

  MsgBox, % Buf

  destroyMonitorHandle(handle)
}

; getMonitorCapabilities()
; source := getMonitorVolume()
; MsgBox, % source
source := setMonitorBrightness(20)

; ; Switching sources~
; #x::
; if(getMonitorInputSource() > 3)
; 	setMonitorInputSource(3)
; else
; 	setMonitorInputSource(17)
; return

