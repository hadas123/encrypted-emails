package finalProject;

import com.intel.util.*;
import com.intel.util.FlashStorage;

public class FlashManger {
	static int RSAKeyFd=0;
	
	
	public static byte[] GetRSAKey() {
		int size=FlashStorage.getFlashDataSize(RSAKeyFd);
		byte[] FlashKey=new byte[size];
		FlashStorage.readFlashData(RSAKeyFd, FlashKey,0);
		return FlashKey;
	
	} 
	public static void SetRSAKey(byte[]Key) {
		FlashStorage.writeFlashData(RSAKeyFd,Key, 0, Key.length);
	} 
	public static boolean existKey() {return (FlashStorage.getFlashDataSize(RSAKeyFd)>0);}




	
	
	
	
	
	
	
}
