import java.io.*;
import java.net.Socket;
import java.util.Base64;




public class ConnectionHandler implements Runnable {
	private Socket server;
	private BufferedReader in;
	
	public ConnectionHandler(Socket server) throws IOException {
		this.setServer(server);
		in = new BufferedReader(new InputStreamReader(server.getInputStream()));
		
	}

	@Override
	public void run() {
		
		try {
			while(true) {
				String serverResponse = null;
				serverResponse = in.readLine();
				if(serverResponse == null) break;
				byte[] decodedMsg = Base64.getDecoder().decode(serverResponse);
				String msg = new String(decodedMsg, "UTF-8");
				System.out.println(msg);
			}
		}catch(IOException e) {
			e.printStackTrace();
		} finally {
			try {
				in.close();
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
		}
		
	}

	public Socket getServer() {
		return server;
	}

	public void setServer(Socket server) {
		this.server = server;
	}

}
