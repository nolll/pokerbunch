import api from '@/api';
import { MessageResponse } from '@/response/MessageResponse';
import { useMutation } from '@tanstack/vue-query';

const clearCache = async (): Promise<MessageResponse> => {
  var response = await api.clearCache();
  return response.data;
};

export const useClearCacheMutation = (onSuccess?: () => void) => {
  return useMutation({
    mutationFn: clearCache,
    onSuccess: () => {
      if (onSuccess) onSuccess();
    },
  });
};
