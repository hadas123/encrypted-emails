package finalProject;

import com.intel.crypto.RsaAlg;

public class Sing {

	public byte[] AddSigneture(byte[] myResponse) {
		RsaAlg Rsa=new GenrateRSAKeys().genrateKey();
		
		byte[] Signeture=new byte[256];
		int length=(int)Rsa.signComplete(myResponse, (short)0, (short)myResponse.length, Signeture, (short)0);
		
		//ByteParser b=new ByteParser();
		//byte[] lenByte=b.convertIntToByteArray(length);
		//Signeture=b.EArray(Signeture, length);
		//return b.Append(lenByte, b.Append(Signeture, myResponse));
		//return  b.Append(Signeture, myResponse);
		return Signeture;
	}

}
