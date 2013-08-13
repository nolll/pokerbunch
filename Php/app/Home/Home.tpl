{extends file="app/Page.tpl"}
{block name='title'}Home{/block}
{block name=page}
	<div class="block gutter">
		<h1>This is Poker Bunch</h1>
	</div>
	<div class="block gutter">
		{if $model->isLoggedIn}
			<p>
				Poker Bunch helps you keep track of the results in your poker homegames.
				Please select one of your bunches, or <a href="{$model->addHomegameUrl->url}">create a new bunch</a>.
			</p>
			<p>
				If you want to join an existing bunch, you will need an invitation from the bunch manager.
			</p>
		{else}
			<p>
				Poker Bunch helps you keep track of the results in your poker homegames.
			</p>
			<p>
				<a href="{$model->loginUrl->url}">Sign in</a> if you already have an account, or <a href="{$model->registerUrl->url}">register</a> to create a bunch and begin inviting players.
			</p>
		{/if}
		{partial view='core\Navigation\Navigation' model=$model->adminNav}
	</div>
{/block}
{block name=footer}
	{include file="app/Site/Financing.tpl"}
{/block}