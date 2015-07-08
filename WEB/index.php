<?php
// In PHP versions earlier than 4.1.0, $HTTP_POST_FILES should be used instead
// of $_FILES.

define('KEY','fdnf[adfnFfjasdf432165');
define('SQL_HOST','');
define('SQL_USER','');
define('SQL_PASS','');
define('SQL_DB','');
define('PROTOCOL_REQUIRIMENT',2);

$protocolVersion = (isset($_POST['protocol']) ? $_POST['protocol'] : 1);
if (PROTOCOL_REQUIRIMENT != $protocolVersion)
	die("ERR\r\nYour client is outdated. Please download latest version from:\r\nhttp://viki.lt/PoE/StasherInstall/");

if (!isset($_POST['key']))
	die("ERR\r\nBad Sync Key! (1)");

$sqlLink = mysqli_connect(SQL_HOST,SQL_USER,SQL_PASS,SQL_DB) or die("ERR\r\n".mysqli_error($sqlLink));
$query = mysqli_query($sqlLink,'SELECT user FROM stash_users WHERE `key`=\''.mysqli_real_escape_string($sqlLink,$_POST['key']).'\'') or die("ERR\r\n".mysqli_error($sqlLink));

if (mysqli_num_rows($query) == 0)
	die("ERR\r\nBad Sync Key! (2)");

$data = mysqli_fetch_array($query);
$user = $data['user'];
$eUser = mysqli_real_escape_string($sqlLink,$user);

if (!isset($_FILES['stash']))
{
	$query = mysqli_query($sqlLink,'SELECT `owner`,`leagueId`,`version` FROM `stash_sync`') or die;
	echo "OK\r\n";
	while ($d = mysqli_fetch_array($query))
	{
		echo $d['owner'].' '.$d['leagueId'].' '.$d['version']."\r\n";	
	}
}
else
{
	$UploadDir = 'stash/';
	$SavePath = $UploadDir . $_POST['owner'] . '.' . $_POST['leagueId'] . '.poe';
	
	if (move_uploaded_file($_FILES['stash']['tmp_name'], $SavePath))
	{
		$eOwner = mysqli_real_escape_string($sqlLink, $_POST['owner']);
		$eLeague = mysqli_real_escape_string($sqlLink, $_POST['leagueId']);
		$eVersion = mysqli_real_escape_string($sqlLink, $_POST['version']);
		mysqli_query($sqlLink,'INSERT INTO `stash_sync` (`owner`,`leagueId`,`version`,`user`) VALUES (\''
		.$eOwner.'\',\''.$eLeague.'\',\''.$eVersion.'\',\''.$eUser.'\')'
		.' ON DUPLICATE KEY UPDATE `version`=\''.$eVersion.'\', `user`=\''.$eUser.'\'');
	  echo "OK\r\nUploaded.";
	} 
	else 
	{
		die("ERR\r\nUpload failed!");
	}

}
?>