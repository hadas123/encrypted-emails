package finalProject;

import com.intel.crypto.RsaAlg;

public class Decrypter {
	
	public byte[] dencrypt(byte[] encryptedContent) {
		RsaAlg Rsa=new GenrateRSAKeys().genrateKey();
		byte[]dencryptedOutput=new byte[(encryptedContent.length)];
		Rsa.decryptComplete(encryptedContent,(short)0,(short)encryptedContent.length, dencryptedOutput, (short)0);
		return dencryptedOutput;
	}
	
		
	
}
