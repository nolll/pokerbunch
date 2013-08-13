{extends file="app/Page.tpl"}
{block name='title'}Sharing{/block}
{block name=full}
	<div class="block gutter">
		<h1>Sharing</h1>
	</div>
	<div class="block gutter">
		<p>
			This is your sharing settings
		</p>
		<h2>Twitter</h2>
		<p>
			{if $model->isSharingToTwitter}
				You are sharing to twitter.
			{else}
				You are not sharing to twitter.
			{/if}
			<a href="{$model->shareToTwitterSettingsUrl->url}">Settings</a>
		</p>
	</div>
{/block}