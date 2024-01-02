import java.io.*;
import java.util.Scanner;

public class Run {

	public static void main(String[] args) throws IOException{
		
		Scanner sc = new Scanner(System.in);
		
		System.out.println("Host or Client?");
		String ans = sc.nextLine();
		
		if(ans.equals("host")) {
			
			System.out.println("Name: ");
			String name = sc.nextLine();
			
			System.out.println("Port: ");
			int port = sc.nextInt();
			
			sc.nextLine();
			
			Server s1 = new Server(name, port);
			s1.run();
			
		}else {
			System.out.println("Name: ");
			String name = sc.nextLine();
			
			System.out.println("IP: ");
			String ip = sc.nextLine();
			
			System.out.println("Port: ");
			int port = sc.nextInt();
			
			sc.nextLine();
			
			Client c1 = new Client(name, ip, port);
			c1.run();
		}
		
		sc.close();
	}
	

}
