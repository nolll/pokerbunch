<?php
namespace integration\Message{

	use core\DateTimeFactory;
	use core\Globalization;

	class FileLoggingMessageSender implements MessageSender{

		private $folderName;

		public function __construct(){
			$this->setFolderName();
		}

		public function send($to, $subject, $body){
			$content = $this->getContents($to, $subject, $body);
			$fileName = $this->getFileName();
			$this->writeFile($content, $fileName);
		}

		private function getFileName(){
			$now = DateTimeFactory::now();
			$fileName = Globalization::formatIsoDateTime($now);
			$fileName = str_replace(' ', '_', $fileName);
			$fileName = str_replace(':', '.', $fileName);
			return $this->folderName . '/' . $fileName . '.txt';
		}

		private function writeFile($content, $fileName){
			$this->ensureFolderExists();
			$file = fopen($fileName, 'w') or die('can\'t open file');
			fwrite($file, $content);
			fclose($file);
		}

		private function getContents($to, $subject, $body){
			$content = 	"TO\r\n" . $to . "\r\n\r\n" .
						"SUBJECT\r\n" . $subject . "\r\n\r\n" .
						"BODY\r\n" . $body;
			return $content;
		}

		private function ensureFolderExists(){
			if(!file_exists($this->folderName)){
				$this->createFolder();
			}
		}

		private function createFolder(){
			mkdir($this->folderName);
		}

		private function setFolderName(){
			$this->folderName = $_SERVER{'DOCUMENT_ROOT'} . '/maillog';
		}

	}

}