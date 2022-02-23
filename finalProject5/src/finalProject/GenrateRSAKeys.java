package finalProject;
import com.intel.crypto.RsaAlg;
public class GenrateRSAKeys {
	
	public RsaAlg genrateKey() {
		RsaAlg Rsa=RsaAlg.create();
		Rsa.setHashAlg(RsaAlg.HASH_TYPE_SHA256);
		Rsa.setPaddingScheme(RsaAlg.PAD_TYPE_PKCS1);
		KeyHandler k=new KeyHandler();
		if(FlashManger.existKey()) {
			byte[] key=FlashManger.GetRSAKey();
			Rsa.setKey(k.getModules(key), (short)0,(short)k.getModules(key).length, k.getPublicExponent(key),(short) 0, (short)k.getPublicExponent(key).length,  k.getPraivteKey(key), (short)0,  (short)k.getPraivteKey(key).length);
		}
		else {
			Rsa.generateKeys((short)256);
			byte[] modulus=new byte[Rsa.getModulusSize()];
			byte[] publicE=new byte[Rsa.getPublicExponentSize()];
			byte[] privateE=new byte[Rsa.getPrivateExponentSize()];
			Rsa.getKey(modulus, (short)0, publicE,  (short)0, privateE,  (short)0);
			FlashManger.SetRSAKey(k.getALLKeys(modulus, publicE, privateE));
			
		}
	return Rsa;	
	}
}
