package finalProject;

public class KeyHandler {
	//Key:
			//|moudles|publicexponent|d--|
			// |256---|4-------------|256|
			
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
		
		return	(ByteArray[startIndex] | (ByteArray[startIndex+1] << 8) | (ByteArray[startIndex+2] << 16) | (ByteArray[startIndex+3] << 24));
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
	public byte[] getALLKeys(byte[] moudles, byte[] publicexponent,byte[] d) {
	
		return Append(Append(moudles,publicexponent),d);
	}
	public byte[] getALLPublicKey(byte[] moudles, byte[] publicexponent ) {
		
		return Append(moudles,publicexponent);}
	
	public byte[] getALLPublicKey(byte[] key) {
		
		return getSubArray(key,0,256+4);
		
	}
		
	public byte[] getModules(byte[] Key) {
		return getSubArray(Key,0,256);
	}
	public byte[] getModulesFromPK(byte[] Key) {
		return getSubArray(Key,0,256);
	}
	public byte[] getPublicExponent(byte[] Key) {
		return getSubArray(Key,256,4);
	}
	public byte[] getPublicExponentFromPK(byte[] Key) {
		return getSubArray(Key,256,4);
	}
	public byte[] getPraivteKey(byte[] Key) {
		
		return getSubArray(Key,256+4,256);
	}

}
