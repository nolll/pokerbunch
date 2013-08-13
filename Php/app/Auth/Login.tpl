{extends file="app/Page.tpl"}
{block name='title'}Sign in{/block}
{block name=full}
	<div class="block gutter">
		<h1>Sign in</h1>
	</div>
	<div class="block gutter">

		<p>
			Please sign in using your username and password. <a href="{$model->forgotPasswordUrl->url}">Forgot password?</a>
		</p>
		<p>
			If you are a new user, please <a href="{$model->addUserUrl->url}">register</a>!
		</p>

		{partial view='app\Site\Errors' model=$model->validationErrors}

		<form method="post">
			<input type="hidden" id="return" name="return" value="{$model->returnUrl->url}"/>
			<fieldset>
				<p>
					<label for="ln">Email or User Name</label>
					<input type="text" id="ln" name="ln" value="{$model->loginName->url}" class="textfield" autocapitalize="off" />
				</p>
				<p>
					<label for="pw">Password</label>
					<input type="password" id="pw" name="pw" class="textfield"/>
				</p>
				<p class="checkbox-layout">
					<label for="remember" class="checkbox-label">Keep me signed in</label>
					<input type="checkbox" id="remember" name="remember"/>
				</p>
			</fieldset>
			<div class="buttons">
				<button type="submit" class="action">Sign in</button>
			</div>
		</form>
	</div>
{/block}