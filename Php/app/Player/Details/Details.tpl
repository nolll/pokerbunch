{extends file="app/Page.tpl"}
{block name='title'}Player Details{/block}
{block name=page}
	<div class="block gutter">
		<h1>{$model->displayName}</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Player\Facts\PlayerFacts' model=$model->playerFactsModel}

		{partial view='app\Player\Achievements\PlayerAchievements' model=$model->playerAchievementsModel}

		{if $model->deleteEnabled}
			<p>
				<a href="{$model->deleteUrl->url}" class="button warning" data-require="delete-confirmation" data-message="Delete Player?">Delete</a>
			</p>
		{/if}
	</div>
{/block}
{block name=aside2}
	<div class="block gutter">
		{if $model->showUserInfo}
			{partial view='app\User\Avatar\Avatar' model=$model->avatarModel}
			<p>
				<a href="{$model->userUrl->url}" class="button">User Profile</a>
			</p>
		{else}
			<p>
				<a href="{$model->invitationUrl->url}" class="button">Invite User</a>
			</p>
		{/if}
	</div>
{/block}