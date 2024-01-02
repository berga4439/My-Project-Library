<?php

//mySQL information for connecting
$servername = "sql5.freemysqlhosting.net";
$database = "sql5667942";
$username = "sql5667942";
$password = "VfCyb3vg3r";


//js does form validation so all values posted correctly
$name = $_POST["name"];
$device = $_POST["device"];
$notes = $_POST["notes"];


//establish connection
$conn = mysqli_connect($servername, $username, $password, $database);
if(!$conn){
	die("Connection failed: " . mysqli_connect_error());
}


//This part was out of scope for the course but I understand what it does

//this starts a new call to the server
$conn->begin_transaction();

try {
    // Creating the 3 insert queries for each table
    $sql1 = "INSERT INTO Tickets(status, notes) VALUES ('IN QUEUE', '$notes');";
    $sql2 = "INSERT INTO names(name) VALUES ('$name');";
    $sql3 = "INSERT INTO devices(device) VALUES ('$device');";

    // Execute the queries
    $conn->query($sql1);
    $conn->query($sql2);
    $conn->query($sql3);

    // Commit the transaction if all queries are successful
	// makes sure every table was filled so they aren't out of sync
    $conn->commit();
    echo "Transaction successful: Data inserted into all tables.";
} catch (Exception $e) {
    // Rollback the transaction if any query fails
    $conn->rollback();
    echo "Transaction failed: " . $e->getMessage();
}

//close connection
mysqli_close($conn);

//Move to the waitlist webpage once INSERT commands are done
header("Location: https://mscs-php.uwstout.edu/2023FA/cs/248/002/berga4439/Assignment%209/waitlist.php");
exit();
?>

<!DOCTYPE html>
<html>
	<head>
		<title>Processing</title>
	</head>
	<body>
		<p>Loading</p>
	</body>
</html>