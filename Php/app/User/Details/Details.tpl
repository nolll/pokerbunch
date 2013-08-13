{extends file="app/Page.tpl"}
{block name='title'}User Details{/block}
{block name=aside1}
	<div class="block gutter">
		{partial view='app\User\Avatar\Avatar' model=$model->avatarModel}
	</div>
{/block}
{block name=page}
	<div class="block gutter">
		<h1>User Profile</h1>
	</div>
	<div class="block gutter">
		<h2>Login Name</h2>
		<p>
			{$model->userName}
		</p>
		<h2>Display Name</h2>
		<p>
			{$model->displayName}
		</p>
		<h2>Real Name</h2>
		<p>
			{$model->realName}
		</p>
		<h2>Email</h2>
		<p>
			{$model->email}
		</p>
		{if $model->showEditLink}
			<p>
				<a href="{$model->editLink->url}">Edit Profile</a>
				{if $model->showPasswordLink}
					<a href="{$model->changePasswordLink->url}">Change Password</a>
				{/if}
			</p>
		{/if}
	</div>
{/block}