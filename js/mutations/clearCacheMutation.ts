import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { useMutation } from '@tanstack/vue-query';

const clearCache = async (): Promise<MessageResponse> => {
  var response = await api.clearCache();
  return response.data;
};

export const useClearCacheMutation = (
  onSuccess?: (response: MessageResponse) => void,
  onError?: (response: MessageResponse) => void
) => {
  return useMutation({
    mutationFn: clearCache,
    onSuccess: onSuccess,
    onError: onError,
  });
};
