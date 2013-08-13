<?php
namespace core\Validation{

	class CompositeValidator extends BaseValidator{

		/** @var Validator[] */
		private $validators;

		private $valid;
		private $validated;

		public function __construct(){
			$this->validators = array();
			$this->validated = false;
			$this->valid = true;
		}

		public function addValidator(Validator $validator){
			$this->validators[] = $validator;
		}

		public function validate(){
			$this->validated = true;
			foreach($this->validators as $validator){
				if(!$validator->isValid()){
					$this->valid = false;
				}
			}
		}

		public function isValid(){
			if(!$this->validated){
				$this->validate();
			}
			$this->ensureErrorArray();
			return $this->valid && count($this->errors) == 0;
		}

		public function getErrors(){
			$this->ensureErrorArray();
			foreach($this->validators as $validator){
				$this->errors = array_merge($this->errors, $validator->getErrors());
			}
			return $this->errors;
		}

	}

}