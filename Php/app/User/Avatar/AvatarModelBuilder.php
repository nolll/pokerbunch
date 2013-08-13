<?php
namespace app\User\Avatar{

	use integration\Avatar\AvatarSize;
	use integration\Avatar\AvatarService;

	class AvatarModelBuilder {

		private $avatarService;

		public function __construct(AvatarService $avatarService){
			$this->avatarService = $avatarService;
		}

		public function build($email, $size = AvatarSize::large){
			$model = new AvatarModel();
			if($email != null){
				$model->avatarEnabled = true;
				$model->avatarUrl = $this->getAvatarUrl($email, $size);
			} else {
				$model->avatarEnabled = false;
			}
			return $model;
		}

		private function getAvatarUrl($email, $size){
			if($size == AvatarSize::small){
				return $this->avatarService->getSmallAvatarUrl($email);
			}
			return $this->avatarService->getLargeAvatarUrl($email);
		}

	}

}