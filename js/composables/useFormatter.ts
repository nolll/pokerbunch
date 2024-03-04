import useBunch from './useBunch';
import format from '@/format';
import useParams from './useParams';

export default function useFormatter() {
  const { slug } = useParams();
  const { localization, bunchReady } = useBunch(slug.value);

  const formatCurrency = (val: number): string => {
    return bunchReady.value ? format.currency(val, localization.value) : val.toString();
  };

  const formatResult = (val: number): string => {
    return bunchReady.value ? format.result(val, localization.value) : val.toString();
  };

  const formatResultWithoutCurrency = (val: number): string => {
    return format.resultWithoutCurrency(val);
  };

  const formatWinrate = (val: number): string => {
    return bunchReady.value ? format.winrate(val, localization.value) : val.toString();
  };

  const formatDuration = (val: number): string => {
    return format.duration(val);
  };

  return {
    formatCurrency,
    formatResult,
    formatResultWithoutCurrency,
    formatWinrate,
    formatDuration,
  };
}
