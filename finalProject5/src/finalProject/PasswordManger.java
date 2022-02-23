package finalProject;
import com.intel.util.DebugPrint;
import com.intel.util.FlashStorage;

public class PasswordManger {
int fd=1;
boolean session=false;
	
	public boolean SetPassword(byte[] password) {
		FlashStorage.writeFlashData(fd,password, 0, password.length);
		return true;
	}
	public  boolean existPassword() {return (FlashStorage.getFlashDataSize(fd)>0);}

	
	public boolean Login(byte[] password) {

		if(!existPassword()) {
			return false;
		}
		byte[] FlashPassword=new byte[60];
		int length=FlashStorage.readFlashData(fd, FlashPassword,0);
		if(length!=password.length)return false;
		for (int i=0; i<length;i++) 
			if ((FlashPassword[i])!=(password[i]))return false;
		
		session=true;
		return true;	
		
	}
	
	public boolean ResetPassword(byte[] password) {
		if(!session)return false;
		SetPassword(password);
		return true;
	}
	
	public boolean LogOut() {
		session= false;
		return true;
	}
	
	
	
	
	
}
