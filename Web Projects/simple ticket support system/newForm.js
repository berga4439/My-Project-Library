function submitForm(){
	var name = document.getElementById("name");
	var nameLbl = document.getElementById("nameLbl");
	
	var device = document.getElementById("device");
	var deviceLbl = document.getElementById("deviceLbl");
	
	var notes = document.getElementById("notes");
	var notesLbl = document.getElementById("notesLbl");
	
	var ticketForm = document.forms["ticketForm"];
	
	if(name.value){
		nameLbl.style.color = "";
	}
	if(device.value){
		deviceLbl.style.color = "";
	}
	if(notes.value){
		notesLbl.style.color = "";
	}
	
	if(!name.value){
		nameLbl.style.color = "red";
	}else if(!device.value){
		deviceLbl.style.color = "red";
	}else if(!notes.value){
		notesLbl.style.color = "red";
	}else{
		if(confirm("Are you sure you want to submit this ticket?")){
			ticketForm.submit();
		}
	}
}