<template>
  <div class="spinner">
    <div class="rect rect1"></div>
    <div class="rect rect2"></div>
    <div class="rect rect3"></div>
    <div class="rect rect4"></div>
    <div class="rect rect5"></div>
  </div>
</template>

<script setup lang="ts"></script>

<style lang="scss">
.spinner {
  margin: 100px auto;
  width: 50px;
  height: 30px;
  text-align: center;
  font-size: 10px;
}

.rect {
  background-color: #333;
  height: 100%;
  width: 6px;
  display: inline-block;
  -webkit-animation: stretchdelay 1.2s infinite ease-in-out;
  animation: stretchdelay 1.2s infinite ease-in-out;
}

.rect2 {
  -webkit-animation-delay: -1.1s;
  animation-delay: -1.1s;
}

.rect3 {
  -webkit-animation-delay: -1s;
  animation-delay: -1s;
}

.rect4 {
  -webkit-animation-delay: -0.9s;
  animation-delay: -0.9s;
}

.rect5 {
  -webkit-animation-delay: -0.8s;
  animation-delay: -0.8s;
}

@-webkit-keyframes stretchdelay {
  0%,
  40%,
  100% {
    -webkit-transform: scaleY(0.4);
  }

  20% {
    -webkit-transform: scaleY(1);
  }
}

@keyframes stretchdelay {
  0%,
  40%,
  100% {
    transform: scaleY(0.4);
    -webkit-transform: scaleY(0.4);
  }

  20% {
    transform: scaleY(1);
    -webkit-transform: scaleY(1);
  }
}
</style>
