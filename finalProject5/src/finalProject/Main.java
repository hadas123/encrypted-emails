package finalProject;

import com.intel.crypto.RsaAlg;
import com.intel.util.*;



//
// Implementation of DAL Trusted Application: finalProject 
//
// **************************************************************************************************
// NOTE:  This default Trusted Application implementation is intended for DAL API Level 7 and above
// **************************************************************************************************

public class Main extends IntelApplet {
	PasswordManger m;
	 public final int SET_PASSWORD =1,
			 			LOGIN= 2,
			 			RESET_PASSWORD=3,
			 			ENCRPT_CONTENT= 4,
			 			DENCRPT_CONTENT= 5,
			 			GET_PUBLIC_KEY = 6,
			 			LOGOUT=7,
			 			SING=8,
			 			VERIFY=9;
     

	public int onInit(byte[] request) {
		 m=new PasswordManger();
		return APPLET_SUCCESS;
	}
	
	
	public int invokeCommand(int commandId, byte[] request) {
		
		 byte[] myResponse=new byte[1]; 
		 myResponse[0]=0;
		switch(commandId) {
		case SET_PASSWORD:
			if(!m.existPassword())
				if(m.SetPassword(request))myResponse[0]=1;
			break;
		case LOGIN://Log-in
			if(m.Login(request))myResponse[0]=1;
			
			break;
		case RESET_PASSWORD://ResetPassword
			if(m.ResetPassword(request))myResponse[0]=1;
			break;
		case ENCRPT_CONTENT:
			myResponse=new Encrypter().encrypt(request);//sender public key
			
		
			break;
		case SING:
			myResponse=new Sing().AddSigneture(request);//my private key
			break;
		case DENCRPT_CONTENT:
		//	 Verify v=new Verify();
		//	byte[] data= v.getEncrptedData(request);
		//	if(new Verify().verify(v.getSignture(request), v.getPublicKey(request), data))//sender public key
		//	{myResponse=new Decrypter().dencrypt(data);}//my private key
			
			myResponse=new Decrypter().dencrypt(request);
			break;	
		case VERIFY:
		Verify v=new Verify();
		if(v.verify(v.getSignture(request),v.getPublicKey(request), v.getEncrptedData(request)))
				myResponse[0]=1;
			break;
		case GET_PUBLIC_KEY:
			
			if(!FlashManger.existKey()) {
				new GenrateRSAKeys().genrateKey();
			}
			KeyHandler key=new KeyHandler();
			myResponse=key.getALLPublicKey(FlashManger.GetRSAKey());
			break;	
		case LOGOUT:
			if(m.LogOut())myResponse[0]=1;
			break;
		}
		
		setResponse(myResponse, 0, myResponse.length);
		setResponseCode(commandId);
		return APPLET_SUCCESS;
	}

	public int onClose() {
		return APPLET_SUCCESS;
	}
}
