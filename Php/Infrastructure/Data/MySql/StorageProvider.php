<?php
namespace Infrastructure\Data\MySql {

	interface StorageProvider {

		function query($sql);

		function execute($sql);

		function executePrepared($sql);

		function getLatestInsertId($success = true);

		function quote($string);

		function boolToInt($bool);

	}

}