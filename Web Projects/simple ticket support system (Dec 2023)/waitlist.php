<?php

//mySQL information for connecting
$servername = "sql5.freemysqlhosting.net";
$database = "sql5667942";
$username = "sql5667942";
$password = "VfCyb3vg3r";

//initiate connection
$conn = mysqli_connect($servername, $username, $password, $database);
if(!$conn){
	die("Connection failed: " . mysqli_connect_error());
}

//create and fill an array of each table entry
$clients = array();
$sql = "SELECT names.name, devices.device, Tickets.status FROM Tickets JOIN names ON Tickets.id = names.name_id JOIN devices ON Tickets.id = devices.device_id;";
$results = mysqli_query($conn,$sql);
while($row = mysqli_fetch_assoc($results)){
	array_push($clients, $row);
}
//end connection
mysqli_close($conn);


?>

<!DOCTYPE html>
<html lang="en">
	<head>
		<title>Waitlist</title>
		<meta charset="UTF-8">
		<link href="newstyle.css" type="text/css" rel="stylesheet"/>
	</head>
	<body>
	<div>
		<h1 class="center">Waitlist</h1>
		<table class="center">
			<tr>
				<th>Place</th>
				<th>Name</th>
				<th>Device</th>
				<th>Status</th>
			</tr>
			<?php
				//make table rows with data from the sql server. 
				//each row uses the count variable to display ordering. If I were to delete an entry in the middle of the table, it would 
				//display correctly
				$count = 1;
				foreach($clients as $client){
					?>
					<tr>
						<td><?=$count?></td>
						<td><?=$client["name"]?></td>
						<td><?=$client["device"]?></td>
						<td><?=$client["status"]?></td>
					</tr>
					<?php
					$count += 1;
				}
			?>
		</table>
	</div>
	</body>
</html>