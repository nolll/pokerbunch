{extends file="app/Page.tpl"}
{block name='title'}Forgot Password{/block}
{block name=full}
	<div class="block gutter">
		<h1>Forgot Password</h1>
	</div>
	<div class="block gutter">
		<p>
			Please enter your email address. You will get a new password by email.
		</p>
		{partial view='app\Site\Errors' model=$model->validationErrors}
		<form method="post" autocomplete="off">
			<fieldset>
				<label>Email</label>
				<p>
					<input type="text" name="email" />
				</p>
			</fieldset>
			<div class="buttons">
				<button class="action" type="submit">Send</button>
			</div>
		</form>
	</div>
{/block}