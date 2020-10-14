<template>
  <q-select
    v-model="lang"
    :options="langOptions"
    :label="$t('language')"
    dense
    borderless
    emit-value
    map-options
    options-dense
  />
</template>

<script>
export default {
  data() {
    return {
      lang: this.$i18n.locale,
      langOptions: [
        { value: 'fr', label: 'Français' },
        { value: 'en-us', label: 'English' },
      ],
    };
  },
  watch: {
    lang(language) {
      this.$i18n.locale = language;
      // Change quasar's language pack too
      // eslint-disable-next-line
      import('quasar/lang/' + language).then(lang => { this.$q.lang.set(lang.default); });
    },
  },
};
</script>
