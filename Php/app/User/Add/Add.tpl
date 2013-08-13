{extends file="app/Page.tpl"}
{block name='title'}Register{/block}
{block name=full}
	<div class="block gutter">
		<h1>Register</h1>
	</div>
	<div class="block gutter">
		<p>
			When you register, a password will be sent to the email address you provide.
			This password can be changed after you log in
		</p>

		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<fieldset>
				<label>Login Name</label>
				<p>
					<input type="text" name="username" value="{$userName}" />
				</p>
				<label>Display Name</label>
				<p>
					<input type="text" name="displayname" value="{$displayName}" />
				</p>
				<label>Email</label>
				<p>
					<input type="text" name="email" value="{$email}" />
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Save</button>
			</div>
		</form>
	</div>
{/block}