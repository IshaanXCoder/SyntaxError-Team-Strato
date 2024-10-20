
import React from "react";
import { Unity, useUnityContext } from "react-unity-webgl";
import './App.css'
import ChatUi from './components/chatUI'
import { useState } from 'react'

      
function App() {
  const { unityProvider } = useUnityContext({
    loaderUrl: "Build/Webgl.loader.js",
    dataUrl: "Build/Webgl.data.unityweb",
    frameworkUrl: "Build/Webgl.framework.js.unityweb",
    codeUrl: "Build/Webgl.wasm.unityweb",
    
  });
  

  return (
    <>
      <ChatUi />

      
     
   <div className="container" style={{
    width:'70%',height:'70%', position: 'absolute', top: 40, left: 120, zIndex: -1,
   }}>
   <Unity unityProvider={unityProvider} style={{ width: 'auto', height: '100%' }} />
   </div>



        
    </>
  );
}

export default App;