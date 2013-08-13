<?php
namespace app\Player{

	use entities\Player;
	use entities\Homegame;
	use core\Validation\CompositeValidator;
	use Domain\Interfaces\PlayerRepository;
	use core\Validation\RequiredValidator;
	use core\Validation\EmailValidator;

	class PlayerValidatorFactoryImpl implements PlayerValidatorFactory{

		private $playerRepository;

		public function __construct(PlayerRepository $playerRepository){
			$this->playerRepository = $playerRepository;
		}

		public function getAddPlayerValidator(Player $player, Homegame $homegame){
			$validator = new CompositeValidator();
			$players = $this->playerRepository->getAll($homegame);
			$validator->addValidator(new RequiredValidator($player->getDisplayName(), 'Display Name can\'t be empty'));
			$validator->addValidator(new UniquePlayerNameValidator($player->getDisplayName(), 'Display Name is in use by someone else', $players));
			return $validator;
		}

		public function getInvitePlayerValidator($email){
			$validator = new CompositeValidator();
			$validator->addValidator(new RequiredValidator($email, 'Email can\'t be empty'));
			$validator->addValidator(new EmailValidator($email, 'The email address is not valid'));
			return $validator;
		}

	}

}