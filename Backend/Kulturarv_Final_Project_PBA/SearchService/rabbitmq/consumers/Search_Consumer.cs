using System; 
using RabbitMQ.Client;

public class Search_Consumer{
	
	public Search_Consumer(){
		Init(); 
	}
	
	public void Init(){
		Console.WriteLine("search consumer added!");
	}
	
	public void Listen_For_Incoming_Requests(){
	    //TODO: Subscribe and handle request messages. 
	}			
	
	public void Validate_Requests(){
		//TODO: Validate the incoming requests. 
	}
}