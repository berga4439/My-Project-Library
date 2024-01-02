var currLocation = window.location.href;

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}
async function run(){
	while(currLocation.includes("youtube")){
		currLocation = window.location.href;
		if(currLocation.includes("shorts/")){
			var index = currLocation.indexOf("shorts");
			var vCode = currLocation.substring(index + 7);
			window.location.replace("https://www.youtube.com/watch?v=" + vCode);
		}
		await sleep(1000);
	}
}

run();