<!DOCTYPE html>
<html lang="en">

	<head>
		<title>{block name='title'}{/block} - Poker Bunch</title>
		<meta charset="utf-8">
		<meta http-equiv="Content-Language" content="en" />
		<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
		<meta name="description" content="" />
		<meta name="author" content="">
		<meta name="keywords" content="" />
		<meta name="viewport" content="width=device-width, initial-scale=1.0">
        <link href="/core/ui/css/compiled/style.css" rel="stylesheet" />
		<link rel="shortcut icon" href="/favicon.ico" />
	</head>

	<body>

		<div class="wrap">

			<header class="page-header">
				<div class="content">
					<div class="logo"><a href="/"><span class="logo-top">Poker</span> <span class="logo-bottom">Bunch</span></a></div>
					{if $model->homegameNavigationModel != null}
						{partial view='app\Homegame\HomegameNavigation' model=$model->homegameNavigationModel}
					{/if}
				</div>
			</header>

			{block name=top hide=true}
				<div class="content-top">
					{$smarty.block.child}
				</div>
			{/block}

			{block name=nav hide=true}
				<div class="content-nav">
					{$smarty.block.child}
				</div>
			{/block}

			<div class="main container">
				{block name=full hide=true}
					<div class="region width3">
						{$smarty.block.child}
					</div>
				{/block}

				{block name=aside1 hide=true}
					<div class="region width1 aside1">
						{$smarty.block.child}
					</div>
				{/block}

				{block name=page hide=true}
					<div class="region width2">
						{$smarty.block.child}
					</div>
				{/block}

				{block name=aside2 hide=true}
					<div class="region width1 aside2">
						{$smarty.block.child}
					</div>
				{/block}
			</div>

			{partial view='core\Navigation\Navigation' model=$model->userNavigationModel}

			<footer>
				<div class="content">
					{block name=footer}{/block}
				</div>
			</footer>

		</div>

		<!--[if lt IE 9]>
			<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
			<script src="/core/ui/js/lib/respond.min.js"></script>
		<![endif]-->
		<script data-main="/core/ui/js/require.config.loader" src="/core/ui/js/lib/require-2.1.2-jquery-1.8.2.min.js"></script>
		{block name=scripts}{/block}

		{partial view='core\Analytics\GoogleAnalytics' model=$model->googleAnalyticsModel}

	</body>

</html>