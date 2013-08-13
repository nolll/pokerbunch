<?php
namespace Infrastructure\Data\MySql {

	class PreparedStatement{

		const UpdateCashgame = 'UPDATE game SET Location = ?, Date = ?, Status = ? WHERE GameID = ?';

	}

}