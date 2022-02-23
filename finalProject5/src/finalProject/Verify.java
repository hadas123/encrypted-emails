package finalProject;
import com.intel.crypto.RsaAlg;
public class Verify {

	public byte[] getPublicKey(byte[] request) {
		ByteParser b=new ByteParser();
		return b.getSubArray(request, 0, 260);
	}
	public byte[] getSignture(byte[] request) {
		ByteParser b=new ByteParser();
		return b.getSubArray(request, 260, 256);
	}
	public byte[] getEncrptedData(byte[] request) {
		ByteParser b=new ByteParser();
		return b.getSubArray(request, 256+260, request.length-(256+260));
	}

	public boolean verify(byte[] signture,byte[] PublicKey,byte[]data) {
		RsaAlg Rsa=new GenrateRSAKeys().genrateKey();
		KeyHandler key=new KeyHandler();
		Rsa.setKey(key.getModulesFromPK(PublicKey), (short)0,(short)key.getModulesFromPK(PublicKey).length , key.getPublicExponentFromPK(PublicKey),(short) 0, (short)key.getPublicExponentFromPK(PublicKey).length);
		
		return	Rsa.verifyComplete(data, (short)0, (short)data.length, signture,(short) 0, (short)signture.length);
		
		
	}

}
