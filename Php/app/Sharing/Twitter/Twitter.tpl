{extends file="app/Page.tpl"}
{block name='title'}Twitter Sharing{/block}
{block name=full}
	<div class="block gutter">
		<h1>Share to Twitter</h1>
	</div>
	<div class="block gutter">
		<form method="post" action="{$model->postUrl->url}">
			{if $model->isSharing}
				<p>
					You are sharing to twitter. Your Twitter user name is &quot;{$model->twitterName}&quot;
				</p>
				<div class="buttons">
					<button class="action" type="submit">Stop Sharing</button>
				</div>
			{else}
				<p>
					You are not sharing to twitter.
				</p>
				<div class="buttons">
					<button class="action" type="submit">Start Sharing</button>
				</div>
			{/if}
		</form>
	</div>
{/block}