package finalProject;
import com.intel.crypto.RsaAlg;
public class Encrypter {
	private byte[] convertIntToByteArray(int intenger) {
		byte[] ByteArray=new byte[4];
		ByteArray[0]=(byte)((intenger) & 0xff);
		ByteArray[1]=(byte)((intenger >> 8) & 0xff);
		ByteArray[2]=(byte)((intenger >> 16) & 0xff);
		ByteArray[3]=(byte)((intenger >> 24) & 0xff);
		return	ByteArray;
	}
	private int converByteArraytToInt(byte[] ByteArray) {
		
		return	(ByteArray[0] | (ByteArray[1] << 8) | (ByteArray[2] << 16) | (ByteArray[3] << 24));
	}
	private int converByteArraytToInt(byte[] ByteArray,int startIndex) {
		
			int len=(ByteArray[startIndex] | (ByteArray[startIndex+1] << 8) | (ByteArray[startIndex+2] << 16) | (ByteArray[startIndex+3] << 24));
			return len;
	}
	private byte[] getSubArray(byte[] Array,int startIndex,int length) {
		byte[] newArray=new byte[length];
		int j=startIndex;
		for(int i=0;i<length;i++) {
			newArray[i]=Array[j];
			j++;
		}
		return newArray;
	}
	private byte[] Append(byte[]first,byte[]second) {
		
		byte[] newArray=new byte[first.length+second.length];
		int j=0;
		for(int i=0;i<first.length;i++) {
			newArray[j]=first[i];
			j++;
			}
		for(int i=0;i<second.length;i++) {
			newArray[j]=second[i];
			j++;
			}
		return newArray;
		
	}
	
	public byte[] encrypt(byte[] ALLpublicKey,byte[] content) {
		KeyHandler key=new KeyHandler();
		return	encrypt(key.getModulesFromPK(ALLpublicKey),key.getPublicExponentFromPK(ALLpublicKey),content);
	}
	
	private byte[] encrypt(byte[] moduls,byte[]publicExponent,byte[] content) {
		RsaAlg Rsa=RsaAlg.create();
		Rsa.setHashAlg(RsaAlg.HASH_TYPE_SHA256);
		Rsa.setPaddingScheme(RsaAlg.PAD_TYPE_PKCS1);
		Rsa.setKey(moduls, (short)0, (short)moduls.length, publicExponent, (short)0, (short)publicExponent.length);
		byte[]encryptedOutput=new byte[ 256*content.length];
		Rsa.encryptComplete(content,  (short)0,  (short)content.length, encryptedOutput, (short)0);
		return encryptedOutput;
	}

	public byte[] encrypt(byte[] request) {
		byte[] publicExponent=getSubArray(request,0,256+4);
		byte[] content=getSubArray(request,256+4,request.length-(256+4));
		return encrypt(publicExponent,content);
	}
}
