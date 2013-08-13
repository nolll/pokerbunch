{extends file="app/Page.tpl"}
{block name='title'}Edit Profile{/block}
{block name=full}
	<div class="block gutter">
		<h1>Edit Profile</h1>
	</div>
	<div class="block gutter">
		{partial view='app\Site\Errors' model=$model->validationErrors}
		<h2>Login Name</h2>
		<p>
			{$model->userName}
		</p>
		<form method="post">
			<fieldset>
				<label>Display Name</label>
				<p>
					<input type="text" name="displayname" value="{$model->displayName}" />
				</p>
				<label>Real Name</label>
				<p>
					<input type="text" name="realname" value="{$model->realName}" />
				</p>
				<label>Email</label>
				<p>
					<input type="text" name="email" value="{$model->email}" />
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Save</button>
			</div>
		</form>
	</div>
{/block}