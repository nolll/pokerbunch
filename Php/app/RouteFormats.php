namespace app{

	class RouteFormats{

		const home = '/';

		const authLogin = '/-/auth/login';
		const authLogout = '/-/auth/logout';

		const cashgameAdd = '/%1$s/cashgame/add';
		const cashgameAddResult = '/%1$s/cashgame/addresult/%2$s';
		const cashgameChart = '/%1$s/cashgame/chart';
		const cashgameChartWithYear = '/%1$s/cashgame/chart/%2$s';
		const cashgameChartJson = '/%1$s/cashgame/chartjson';
		const cashgameChartJsonWithYear = '/%1$s/cashgame/chartjson/%2$s';
		const cashgameDelete = '/%1$s/cashgame/delete/%2$s';
		const cashgameDetails = '/%1$s/cashgame/details/%2$s';
		const cashgameDetailsChartJson = '/%1$s/cashgame/detailschartjson/%2$s';
		const cashgameEdit = '/%1$s/cashgame/edit/%2$s';
		const cashgameEnd = '/%1$s/cashgame/end';
		const cashgameIndex = '/%1$s/cashgame/index';
		const cashgameLeaderboard = '/%1$s/cashgame/leaderboard';
		const cashgameLeaderboardWithYear = '/%1$s/cashgame/leaderboard/%2$s';
		const cashgameMatrix = '/%1$s/cashgame/matrix';
		const cashgameMatrixWithYear = '/%1$s/cashgame/matrix/%2$s';
		const cashgameListing = '/%1$s/cashgame/listing';
		const cashgameListingWithYear = '/%1$s/cashgame/listing/%2$s';
		const cashgameFacts = '/%1$s/cashgame/facts';
		const cashgameFactsWithYear = '/%1$s/cashgame/facts/%2$s';
		const cashgameAction = '/%1$s/cashgame/action/%2$s/%3$s';
		const cashgameActionChartJson = '/%1$s/cashgame/actionchartjson/%2$s/%3$s';
		const cashgameBuyin = '/%1$s/cashgame/buyin/%2$s';
		const cashgameReport = '/%1$s/cashgame/report/%2$s';
		const cashgameCashout = '/%1$s/cashgame/cashout/%2$s';
		const cashgamePublish = '/%1$s/cashgame/publish/%2$s';
		const cashgameRemoveResult = '/%1$s/cashgame/removeresult/%2$s';
		const cashgameUnpublish = '/%1$s/cashgame/unpublish/%2$s';
		const runningCashgame = '/%1$s/cashgame/running';
		const cashgameCheckpointDelete = '/%1$s/cashgame/deletecheckpoint/%2$s/%3$s/%4$s';

		const changePassword = '/-/password/change';
		const changePasswordConfirmation = '/-/password/changed';

		const forgotPassword = '/-/password/forgot';
		const forgotPasswordConfirmation = '/-/password/sent';

		const homegameAdd = '/-/game/add';
		const homegameAddConfirmation = '/-/game/created';
		const homegameDetails = '/%1$s/game/details';
		const homegameEdit = '/%1$s/game/edit';
		const homegameJoin = '/%1$s/game/join';
		const homegameJoinConfirmation = '/%1$s/game/joined';
		const homegameListing = '/-/game/listing';

		const playerAdd = '/%1$s/player/add';
		const playerAddConfirmation = '/%1$s/player/created';
		const playerDelete = '/%1$s/player/delete/%2$s';
		const playerDetails = '/%1$s/player/details/%2$s';
		const playerIndex = '/%1$s/player/index';
		const playerInvite = '/%1$s/player/invite/%2$s';
		const playerInviteConfirmation = '/%1$s/player/invited/%2$s';

		const sharingSettings = '/-/sharing';

		const twitterCallback = '%1$s/-/sharing/twittercallback';
		const twitterSettings = '/-/sharing/twitter';
		const twitterStartShare = '/-/sharing/twitterstart';
		const twitterStopShare = '/-/sharing/twitterstop';

		const userAdd = '/-/user/add';
		const userAddConfirmation = '/-/user/created';
		const userDetails = '/-/user/details/%1$s';
		const userEdit = '/-/user/edit/%1$s';
		const userListing = '/-/user/listing';

	}

}